using System;
using System.Linq;
using System.Reflection.Metadata;
using AutoMapper;
using Labo_Cts_backend.Application.DTOs.Request;
using Labo_Cts_backend.Application.IServices;
using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.IRepositories;
using Labo_Cts_backend.Infrastructure.Context;
using Labo_Cts_backend.Infrastructure.Repositories;
using Labo_Cts_backend.Shared.IServices;
using Labo_Cts_backend.Shared.Services;

namespace Labo_Cts_backend.Application.Services
{
    public class GammeManagerService : IGammeManagerService
    {
        private readonly IGammesChimiquesAlternativeRepository _alternativeRepository;
        private readonly IGammesChimiquesVersionRepository _versionRepository;
        private readonly IParametresVersionRepository _parametresRepository;
        private readonly IArticlesVersionRepository _articlesRepository;
        private readonly IBainRepository _bainRepository;
        private readonly IMapper _mapper;
        private readonly ICommonService _commonService;

        public GammeManagerService(
            IGammesChimiquesAlternativeRepository alternativeRepository,
            IGammesChimiquesVersionRepository versionRepository,
            IParametresVersionRepository parametresRepository,
            IArticlesVersionRepository articlesRepository,
            IBainRepository bainRepository,
            IMapper mapper,
            ICommonService commonService)
        {
            _alternativeRepository = alternativeRepository;
            _versionRepository = versionRepository;
            _parametresRepository = parametresRepository;
            _articlesRepository = articlesRepository;
            _bainRepository = bainRepository;
            _mapper = mapper;
            _commonService = commonService;
        }

        public async Task<GammesChimiquesAlternative> CreateAlternativeAsync(AlternativeCreateDto alt, int bainCode)
        {
            var alternativesOfBain = await _alternativeRepository.GetByCodeBainAsync(bainCode);
            var alternative = _mapper.Map<GammesChimiquesAlternative>(alt);
            alternative.CodeBain = bainCode;
            alternative.Nom = "Alternative " + (alternativesOfBain.Count()+1);
            alternative.DateDebutValidite = _commonService.GetLocalTime("Africa/Tunis");
            _commonService.UpdateTimeAndUserForUpdateFields(alternative, "AdminUser", true);
            return await _alternativeRepository.AddAsync(alternative);
        }

        public async Task AddParametersToVersionAsync(IEnumerable<ParametreVersionCreateDto> parametres, int versionId)
        {
            foreach (var param in parametres)
            {
                var entity = _mapper.Map<ParametresVersion>(param);
                entity.IdGammeVersion = versionId;
                _commonService.UpdateTimeAndUserForUpdateFields(entity, "AdminUser", true);
                await _parametresRepository.AddAsync(entity);
            }
        }

        public async Task AddArticlesToVersionAsync(IEnumerable<ArticleVersionCreateDto> articles, int versionId)
        {
            foreach (var article in articles)
            {
                var entity = _mapper.Map<ArticlesVersion>(article);
                entity.IdGammeVersion = versionId;
                _commonService.UpdateTimeAndUserForUpdateFields(entity, "AdminUser", true);
                await _articlesRepository.AddAsync(entity);
            }
        }

        public async Task UpdateBainActifAlternativeAndVersionAsync(int bainCode, int idAlternative, int idVersion)
        {
            var bain = await _bainRepository.GetByCodeAsync(bainCode);
            bain.IdAlternativeGammeChimiqueActif = idAlternative;
            bain.IdVersionGammeChimiqueActif = idVersion;
            await _bainRepository.UpdateAsync(bain);
            var bains = await _bainRepository.GetAllByBainReferenceAsync(bainCode);
            foreach(var b in bains)
            {
                b.IdAlternativeGammeChimiqueActif = idAlternative;
                b.IdVersionGammeChimiqueActif= idVersion;
                await _bainRepository.UpdateAsync(b);
            }
        }

        public async Task<bool> HasAlternativeChanges(AlternativeUpdateDto updatedAlternative, GammesChimiquesAlternative existingAlternative)
        {
            bool hasAlternativeChanges = updatedAlternative.EstParDefaut != existingAlternative.EstParDefaut;
            return hasAlternativeChanges;
        }

        public async Task ChangeAlternativeDefaultBainValue(AlternativeUpdateDto updatedAlternative, GammesChimiquesAlternative existingAlternative)
        {
            existingAlternative.EstParDefaut = updatedAlternative.EstParDefaut;
            var latestVersion = (await _versionRepository.GetByIdAlternative(existingAlternative.Id))
                .OrderByDescending(v => v.DateDebutValidite)
                .FirstOrDefault();

            _commonService.UpdateTimeAndUserForUpdateFields(existingAlternative, "AdminUser", false);
            if (existingAlternative.EstParDefaut)
            {
                await UpdateBainActifAlternativeAndVersionAsync(existingAlternative.CodeBain, existingAlternative.Id, latestVersion.Id);
            }
            await _alternativeRepository.UpdateAsync(existingAlternative);
        }

        public async Task<bool> HasChanges(object updatedDto)
        {
            switch (updatedDto)
            {
                case AlternativeUpdateDto alternative:
                    {
                        var version = await _versionRepository.GetLatestVersionOfAlternative(alternative.Id);
                        if (version == null)
                            return true;

                        bool parametersChanged = HaveListsChanged(
                            alternative.Parametres,
                            version.ParametresVersions,
                            ParameterComparer);
                        bool articlesChanged = HaveListsChanged(
                            alternative.Articles,
                            version.ArticlesVersions,
                            ArticleComparer);

                        return parametersChanged || articlesChanged;
                    }
                case GammeChimiqueVersionCreateDto versionDto:
                    {
                        var version = await _versionRepository.GetVersionById((int)versionDto.Id);
                        if (version == null)
                            return true;

                        bool parametersChanged = HaveListsChanged(
                            versionDto.ParametresVersions,
                            version.ParametresVersions,
                            ParameterComparer);
                        bool articlesChanged = HaveListsChanged(
                            versionDto.ArticlesVersions,
                            version.ArticlesVersions,
                            ArticleComparer);

                        return parametersChanged || articlesChanged;
                    }
                default:
                    throw new ArgumentException("Type de DTO non supporté", nameof(updatedDto));
            }
        }

        public async Task MarkVersionAsEnded(int alternativeId)
        {
            var alternative = await _alternativeRepository.GetByIdAsync(alternativeId);
            //var bain = await _bainRepository.GetByCodeAsync(alternative.CodeBain);
            var latestVersion = (await _versionRepository.GetByIdAlternative(alternativeId))
                .OrderByDescending(v => v.DateDebutValidite)
                .FirstOrDefault();

            if (latestVersion != null)
            {
                latestVersion.DateFinValidite = _commonService.GetLocalTime("Africa/Tunis");
                var updatedEntityVersion = await _commonService.UpdateTimeAndUserForUpdateFields(latestVersion, "AdminUser", false);
                var updatedEntityAlternative = await _commonService.UpdateTimeAndUserForUpdateFields(alternative, "AdminUser"  , false);
                await _versionRepository.UpdateAsync(updatedEntityVersion);
                await _alternativeRepository.UpdateAsync(updatedEntityAlternative);
            }
        }

        public async Task<GammesChimiquesVersion> CreateVersionWithUpdates(AlternativeUpdateDto updatedAlternative, int alternativeId)
        {
            // Utilisation directe des paramètres et articles du DTO
            var versionAdded = await CreateVersionWithAssociates(
                alternativeId,
                updatedAlternative.Parametres,
                updatedAlternative.Articles);

            // Mise à jour additionnelle si l'alternative est par défaut
            var alternative = await _alternativeRepository.GetByIdAsync(alternativeId);
            if (alternative.EstParDefaut)
            {
                await UpdateBainActifAlternativeAndVersionAsync(alternative.CodeBain, alternative.Id, versionAdded.Id);
            }
            return versionAdded;
        }

        public async Task<GammesChimiquesVersion> CreateVersionWithArticlesAndParameters(GammesChimiquesVersion sourceVersion, int alternativeId)
        {
            // Mapping des paramètres et articles depuis la version source
            var parametres = _mapper.Map<IEnumerable<ParametreVersionCreateDto>>(sourceVersion.ParametresVersions);
            var articles = _mapper.Map<IEnumerable<ArticleVersionCreateDto>>(sourceVersion.ArticlesVersions);

            return await CreateVersionWithAssociates(alternativeId, parametres, articles);
        }


        /// <summary>
        /// Compare deux listes en vérifiant la présence de chaque élément selon le comparateur fourni.
        /// Retourne true si les listes diffèrent.
        /// </summary>
        private bool HaveListsChanged(
        IEnumerable<dynamic> updatedList,
        IEnumerable<dynamic> existingList,
        Func<dynamic, dynamic, bool> comparer)
        {
            var updatedArray = updatedList.ToArray();
            var existingArray = existingList.ToArray();

            if (updatedArray.Length != existingArray.Length)
                return true;

            if (!updatedArray.All(u => existingArray.Any(e => comparer(u, e))))
                return true;

            if (!existingArray.All(e => updatedArray.Any(u => comparer(u, e))))
                return true;

            return false;
        }

        /// <summary>
        /// Compare deux objets "paramètre" en utilisant leurs propriétés communes.
        /// </summary>
        private bool ParameterComparer(dynamic upd, dynamic exist)
        {
            return upd.IdParametreChimique == exist.IdParametreChimique &&
                   upd.ValeurMin == exist.ValeurMin &&
                   upd.ValeurMax == exist.ValeurMax &&
                   upd.Valeur == exist.Valeur;
        }

        /// <summary>
        /// Compare deux objets "article" en utilisant leurs propriétés communes.
        /// </summary>
        private bool ArticleComparer(dynamic upd, dynamic exist)
        {
            return upd.CodeArticle == exist.CodeArticle &&
                   upd.ValeurMin == exist.ValeurMin &&
                   upd.ValeurMax == exist.ValeurMax &&
                   upd.Valeur == exist.Valeur;
        }

        /// <summary>
        /// Crée une nouvelle version pour une alternative et y ajoute les paramètres et articles associés.
        /// </summary>
        /// <param name="alternativeId">Identifiant de l'alternative.</param>
        /// <param name="parametres">Liste des paramètres à associer.</param>
        /// <param name="articles">Liste des articles à associer.</param>
        /// <returns>La version créée et enregistrée.</returns>
        private async Task<GammesChimiquesVersion> CreateVersionWithAssociates(
            int alternativeId,
            IEnumerable<ParametreVersionCreateDto> parametres,
            IEnumerable<ArticleVersionCreateDto> articles)
        {
            // Récupère les versions existantes pour déterminer le numéro de version
            var versionsOfAlternative = await _versionRepository.GetByIdAlternative(alternativeId);

            var newVersion = new GammesChimiquesVersion
            {
                Nom = "Version " + (versionsOfAlternative.Count() + 1),
                DateDebutValidite = _commonService.GetLocalTime("Africa/Tunis"),
                IdGammeAlternative = alternativeId,
            };
            var updatedEntityAlternative = await _commonService.UpdateTimeAndUserForUpdateFields(newVersion, "AdminUser", true);
            // Ajoute la nouvelle version
            var versionAdded = await _versionRepository.AddAsync(newVersion);

            // Ajoute les paramètres et articles associés
            await AddParametersToVersionAsync(parametres, versionAdded.Id);
            await AddArticlesToVersionAsync(articles, versionAdded.Id);

            return versionAdded;
        }
    }

}
