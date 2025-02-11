using System;
using System.Collections.Generic;
using Labo_Cts_backend.Domain.Entities;
using Labo_Cts_backend.Domain.Vues;
using Microsoft.EntityFrameworkCore;

namespace Labo_Cts_backend.Infrastructure.Context;

public partial class LaboratoireCtsContext : DbContext
{
    public LaboratoireCtsContext()
    {
    }

    public LaboratoireCtsContext(DbContextOptions<LaboratoireCtsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticlesVersion> ArticlesVersions { get; set; }

    public virtual DbSet<Bain> Bains { get; set; }

    public virtual DbSet<CentresDeCharge> CentresDeCharges { get; set; }

    public virtual DbSet<DemandesInterventionsAnalysis> DemandesInterventionsAnalyses { get; set; }

    public virtual DbSet<DemandesInterventionsHull> DemandesInterventionsHulls { get; set; }

    public virtual DbSet<DemandesInterventionsRenouvellement> DemandesInterventionsRenouvellements { get; set; }

    public virtual DbSet<Depot> Depots { get; set; }

    public virtual DbSet<Emplacement> Emplacements { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<EntetesDemandesInterventionsAnalysis> EntetesDemandesInterventionsAnalyses { get; set; }

    public virtual DbSet<EntetesDemandesInterventionsHull> EntetesDemandesInterventionsHulls { get; set; }

    public virtual DbSet<EntetesMouvementsStock> EntetesMouvementsStocks { get; set; }

    public virtual DbSet<GammesChimiquesAlternative> GammesChimiquesAlternatives { get; set; }

    public virtual DbSet<GammesChimiquesVersion> GammesChimiquesVersions { get; set; }

    public virtual DbSet<Instruction> Instructions { get; set; }

    public virtual DbSet<InstructionsVersion> InstructionsVersions { get; set; }

    public virtual DbSet<LignesDemandesInterventionsAnalysis> LignesDemandesInterventionsAnalyses { get; set; }

    public virtual DbSet<LignesDemandesInterventionsHull> LignesDemandesInterventionsHulls { get; set; }

    public virtual DbSet<LignesMouvementsStock> LignesMouvementsStocks { get; set; }

    public virtual DbSet<ParametresChimique> ParametresChimiques { get; set; }

    public virtual DbSet<ParametresVersion> ParametresVersions { get; set; }

    public virtual DbSet<PlansDemandesIntervention> PlansDemandesInterventions { get; set; }

    public virtual DbSet<PostesDeCharge> PostesDeCharges { get; set; }

    public virtual DbSet<RatioArticle> RatioArticles { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<Societe> Societes { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<TablesDiversesDefinition> TablesDiversesDefinitions { get; set; }

    public virtual DbSet<TablesDiversesDonnee> TablesDiversesDonnees { get; set; }

    public virtual DbSet<BainReferenceAlternativeView> BainsReferenceAlternatives { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=IS-SKETATA;Database=LaboratoireCTS;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.ArticleDependance)
                .HasMaxLength(10)
                .HasColumnName("articleDependance");
            entity.Property(e => e.Categorie)
                .HasMaxLength(5)
                .HasColumnName("categorie");
            entity.Property(e => e.CleRecherche)
                .HasMaxLength(50)
                .HasColumnName("cleRecherche");
            entity.Property(e => e.Coefficient1)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("coefficient1");
            entity.Property(e => e.Coefficient2)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("coefficient2");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .HasColumnName("designation");
            entity.Property(e => e.EstRatio).HasColumnName("estRatio");
            entity.Property(e => e.Formule).HasColumnName("formule");
            entity.Property(e => e.MethodeAnalyse)
                .HasMaxLength(20)
                .HasColumnName("methodeAnalyse");
            entity.Property(e => e.SeuilMinimum).HasColumnName("seuilMinimum");
            entity.Property(e => e.Statut).HasColumnName("statut");
            entity.Property(e => e.UniteAnalyse)
                .HasMaxLength(50)
                .HasColumnName("uniteAnalyse");
            entity.Property(e => e.UniteConditionnement)
                .HasMaxLength(5)
                .HasColumnName("uniteConditionnement");
            entity.Property(e => e.UniteStock)
                .HasMaxLength(5)
                .HasColumnName("uniteStock");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");
            entity.Property(e => e.ValeurConversionUniteStockUniteCond)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("valeurConversionUniteStockUniteCond");
            entity.Property(e => e.ValeurRatioMaximale).HasColumnName("valeurRatioMaximale");
            entity.Property(e => e.ValeurRatioMinimale).HasColumnName("valeurRatioMinimale");

            entity.HasOne(d => d.ArticleDependanceNavigation).WithMany(p => p.InverseArticleDependanceNavigation)
                .HasForeignKey(d => d.ArticleDependance)
                .HasConstraintName("FK_Articles_ArticlesDependance");
        });

        modelBuilder.Entity<ArticlesVersion>(entity =>
        {
            entity.HasKey(e => new { e.IdGammeVersion, e.CodeArticle }).HasName("PK_Affectation_Articles_Versions");

            entity.ToTable("Articles_Versions");

            entity.Property(e => e.IdGammeVersion).HasColumnName("idGammeVersion");
            entity.Property(e => e.CodeArticle)
                .HasMaxLength(10)
                .HasColumnName("codeArticle");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");
            entity.Property(e => e.Valeur)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valeur");
            entity.Property(e => e.ValeurMax)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valeurMax");
            entity.Property(e => e.ValeurMin)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valeurMin");

            entity.HasOne(d => d.CodeArticleNavigation).WithMany(p => p.ArticlesVersions)
                .HasForeignKey(d => d.CodeArticle)
                .HasConstraintName("FK_AffectationArticlesVersions_Articles");

            entity.HasOne(d => d.IdGammeVersionNavigation).WithMany(p => p.ArticlesVersions)
                .HasForeignKey(d => d.IdGammeVersion)
                .HasConstraintName("FK_AffectationArticlesVersions_Versions");
        });

        modelBuilder.Entity<Bain>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.Actif).HasColumnName("actif");
            entity.Property(e => e.BainReferenceCode).HasColumnName("bainReferenceCode");
            entity.Property(e => e.CodePosteCharge)
                .HasMaxLength(10)
                .HasColumnName("codePosteCharge");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.DimensionHauteur).HasColumnName("dimensionHauteur");
            entity.Property(e => e.DimensionLargeur).HasColumnName("dimensionLargeur");
            entity.Property(e => e.DimensionLongueur).HasColumnName("dimensionLongueur");
            entity.Property(e => e.Emplacement)
                .HasMaxLength(40)
                .HasColumnName("emplacement");
            entity.Property(e => e.EstReference).HasColumnName("estReference");
            entity.Property(e => e.IdAlternativeGammeChimiqueActif).HasColumnName("idAlternativeGammeChimiqueActif");
            entity.Property(e => e.IdInstructionVersionActif).HasColumnName("idInstructionVersionActif");
            entity.Property(e => e.IdVersionGammeChimiqueActif).HasColumnName("idVersionGammeChimiqueActif");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.BainReferenceCodeNavigation).WithMany(p => p.InverseBainReferenceCodeNavigation)
                .HasForeignKey(d => d.BainReferenceCode)
                .HasConstraintName("FK_Bain_BainReference");
        });

        modelBuilder.Entity<CentresDeCharge>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Centres_De_Charge");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .HasColumnName("designation");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");
        });

        modelBuilder.Entity<DemandesInterventionsAnalysis>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_Demandes_Interventions");

            entity.ToTable("Demandes_Interventions_Analyses");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.DateHeureExecution)
                .HasColumnType("datetime")
                .HasColumnName("dateHeureExecution");
            entity.Property(e => e.DateHeureLancement)
                .HasColumnType("datetime")
                .HasColumnName("dateHeureLancement");
            entity.Property(e => e.IdPlanDemandeIntervention).HasColumnName("idPlanDemandeIntervention");
            entity.Property(e => e.Statut)
                .HasMaxLength(50)
                .HasColumnName("statut");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.IdPlanDemandeInterventionNavigation).WithMany(p => p.DemandesInterventionsAnalyses)
                .HasForeignKey(d => d.IdPlanDemandeIntervention)
                .HasConstraintName("FK_DemandesInterventions_PlanDemande");
        });

        modelBuilder.Entity<DemandesInterventionsHull>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Demandes_Interventions_Hull");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.DateHeureExecution)
                .HasColumnType("datetime")
                .HasColumnName("dateHeureExecution");
            entity.Property(e => e.DateHeureLancement)
                .HasColumnType("datetime")
                .HasColumnName("dateHeureLancement");
            entity.Property(e => e.IdPlanDemandeIntervention).HasColumnName("idPlanDemandeIntervention");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasColumnName("statut");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.IdPlanDemandeInterventionNavigation).WithMany(p => p.DemandesInterventionsHulls)
                .HasForeignKey(d => d.IdPlanDemandeIntervention)
                .HasConstraintName("FK_Demandes_Interventions_Hull_PlansDemandesInterventions");
        });

        modelBuilder.Entity<DemandesInterventionsRenouvellement>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Demandes_Interventions_Renouvellements");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CodeBain).HasColumnName("codeBain");
            entity.Property(e => e.CommentaireExecutePar).HasColumnName("commentaireExecutePar");
            entity.Property(e => e.CommentaireResponsable).HasColumnName("commentaireResponsable");
            entity.Property(e => e.CommentaireSuperviseur).HasColumnName("commentaireSuperviseur");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.DateHeureExecution)
                .HasColumnType("datetime")
                .HasColumnName("dateHeureExecution");
            entity.Property(e => e.DateHeureLancement)
                .HasColumnType("datetime")
                .HasColumnName("dateHeureLancement");
            entity.Property(e => e.ExeDat)
                .HasColumnType("datetime")
                .HasColumnName("exeDat");
            entity.Property(e => e.ExecutePar).HasColumnName("executePar");
            entity.Property(e => e.IdPlanDemandeIntervention).HasColumnName("idPlanDemandeIntervention");
            entity.Property(e => e.Responsable).HasColumnName("responsable");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasColumnName("statut");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeBainNavigation).WithMany(p => p.DemandesInterventionsRenouvellements)
                .HasForeignKey(d => d.CodeBain)
                .HasConstraintName("FK_DemandesInterventionsRenouvellements_Bains");
        });

        modelBuilder.Entity<Depot>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_depot");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CodeSite)
                .HasMaxLength(10)
                .HasColumnName("codeSite");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .HasColumnName("designation");
            entity.Property(e => e.EstActif)
                .HasMaxLength(50)
                .HasColumnName("estActif");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeSiteNavigation).WithMany(p => p.Depots)
                .HasForeignKey(d => d.CodeSite)
                .HasConstraintName("FK_Depots_Sites");
        });

        modelBuilder.Entity<Emplacement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Emplacements_1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CodeDepot)
                .HasMaxLength(10)
                .HasColumnName("codeDepot");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .HasColumnName("designation");
            entity.Property(e => e.EstActif)
                .HasMaxLength(10)
                .HasColumnName("estActif");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeDepotNavigation).WithMany(p => p.Emplacements)
                .HasForeignKey(d => d.CodeDepot)
                .HasConstraintName("FK_Emplacements_Depots");
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.Matricule).HasName("PK_employes");

            entity.Property(e => e.Matricule)
                .HasMaxLength(5)
                .HasColumnName("matricule");
            entity.Property(e => e.CodeCentreCharge)
                .HasMaxLength(10)
                .HasColumnName("codeCentreCharge");
            entity.Property(e => e.CompteLdap).HasColumnName("compteLDAP");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Fonction)
                .HasMaxLength(20)
                .HasColumnName("fonction");
            entity.Property(e => e.MotDePasse).HasColumnName("motDePasse");
            entity.Property(e => e.NomPrenom)
                .HasMaxLength(50)
                .HasColumnName("nomPrenom");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeCentreChargeNavigation).WithMany(p => p.Employes)
                .HasForeignKey(d => d.CodeCentreCharge)
                .HasConstraintName("FK_Employes_CentresDeCharge");
        });

        modelBuilder.Entity<EntetesDemandesInterventionsAnalysis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Entetes_Demandes_Interventions_Analyses_1");

            entity.ToTable("Entetes_Demandes_Interventions_Analyses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodeBain).HasColumnName("codeBain");
            entity.Property(e => e.CodeDemandeInterventionAnalyse)
                .HasMaxLength(10)
                .HasColumnName("codeDemandeInterventionAnalyse");
            entity.Property(e => e.CommenataireResponsable).HasColumnName("commenataireResponsable");
            entity.Property(e => e.CommentaireExecutePar).HasColumnName("commentaireExecutePar");
            entity.Property(e => e.CommentaireSuperviseur).HasColumnName("commentaireSuperviseur");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.ExecutePar).HasColumnName("executePar");
            entity.Property(e => e.Responsable).HasColumnName("responsable");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasColumnName("statut");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeBainNavigation).WithMany(p => p.EntetesDemandesInterventionsAnalyses)
                .HasForeignKey(d => d.CodeBain)
                .HasConstraintName("FK_EntetesDemandesInterventionsAnalyses_Bains");
        });

        modelBuilder.Entity<EntetesDemandesInterventionsHull>(entity =>
        {
            entity.ToTable("Entetes_Demandes_Interventions_Hull");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodeBain).HasColumnName("codeBain");
            entity.Property(e => e.CodeDemandeInterventionAnalyse)
                .HasMaxLength(10)
                .HasColumnName("codeDemandeInterventionAnalyse");
            entity.Property(e => e.CommentaireExecutePar).HasColumnName("commentaireExecutePar");
            entity.Property(e => e.CommentaireResponsable).HasColumnName("commentaireResponsable");
            entity.Property(e => e.CommentaireSuperviseur).HasColumnName("commentaireSuperviseur");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.ExecutePar).HasColumnName("executePar");
            entity.Property(e => e.Responsable).HasColumnName("responsable");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasColumnName("statut");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeBainNavigation).WithMany(p => p.EntetesDemandesInterventionsHulls)
                .HasForeignKey(d => d.CodeBain)
                .HasConstraintName("FK_EntetesDemandesInterventionsHull_Bains");
        });

        modelBuilder.Entity<EntetesMouvementsStock>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_Entetes_Mouvements");

            entity.ToTable("Entetes_Mouvements_Stock");

            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.CodeDepot)
                .HasMaxLength(10)
                .HasColumnName("codeDepot");
            entity.Property(e => e.CodeSite)
                .HasMaxLength(10)
                .HasColumnName("codeSite");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.DateImputation)
                .HasColumnType("datetime")
                .HasColumnName("dateImputation");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .HasColumnName("designation");
            entity.Property(e => e.PieceOrigine)
                .HasMaxLength(10)
                .HasColumnName("pieceOrigine");
            entity.Property(e => e.Statut)
                .HasMaxLength(50)
                .HasColumnName("statut");
            entity.Property(e => e.TypeMvt)
                .HasMaxLength(5)
                .HasColumnName("typeMvt");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeDepotNavigation).WithMany(p => p.EntetesMouvementsStocks)
                .HasForeignKey(d => d.CodeDepot)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntetesMouvements_Depots");

            entity.HasOne(d => d.CodeSiteNavigation).WithMany(p => p.EntetesMouvementsStocks)
                .HasForeignKey(d => d.CodeSite)
                .HasConstraintName("FK_EntetesMouvements_Sites");
        });

        modelBuilder.Entity<GammesChimiquesAlternative>(entity =>
        {
            entity.ToTable("Gammes_Chimiques_Alternatives");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodeBain).HasColumnName("codeBain");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.DateDebutValidite)
                .HasColumnType("datetime")
                .HasColumnName("dateDebutValidite");
            entity.Property(e => e.DateFinValidité)
                .HasColumnType("datetime")
                .HasColumnName("dateFinValidité");
            entity.Property(e => e.EstParDefaut).HasColumnName("estParDefaut");
            entity.Property(e => e.Nom).HasColumnName("nom");
            entity.Property(e => e.NomBain).HasColumnName("nomBain");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeBainNavigation).WithMany(p => p.GammesChimiquesAlternatives)
                .HasForeignKey(d => d.CodeBain)
                .HasConstraintName("FK_Gamme_Bain");
        });

        modelBuilder.Entity<GammesChimiquesVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Versions_Gammes_Chimiques");

            entity.ToTable("Gammes_Chimiques_Versions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.DateDebutValidite)
                .HasColumnType("datetime")
                .HasColumnName("dateDebutValidite");
            entity.Property(e => e.DateFinValidite)
                .HasColumnType("datetime")
                .HasColumnName("dateFinValidite");
            entity.Property(e => e.IdGammeAlternative).HasColumnName("idGammeAlternative");
            entity.Property(e => e.Nom).HasColumnName("nom");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.IdGammeAlternativeNavigation).WithMany(p => p.GammesChimiquesVersions)
                .HasForeignKey(d => d.IdGammeAlternative)
                .HasConstraintName("FK_Version_Alternative");
        });

        modelBuilder.Entity<Instruction>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdInstructionVersion).HasColumnName("idInstructionVersion");
            entity.Property(e => e.Nom).HasColumnName("nom");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.IdInstructionVersionNavigation).WithMany(p => p.Instructions)
                .HasForeignKey(d => d.IdInstructionVersion)
                .HasConstraintName("FK_Instructions_InstructionsVersions");
        });

        modelBuilder.Entity<InstructionsVersion>(entity =>
        {
            entity.ToTable("Instructions_Versions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodeBain).HasColumnName("codeBain");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.DateDebutValidite)
                .HasColumnType("datetime")
                .HasColumnName("dateDebutValidite");
            entity.Property(e => e.DateFinValidite)
                .HasColumnType("datetime")
                .HasColumnName("dateFinValidite");
            entity.Property(e => e.Nom).HasColumnName("nom");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeBainNavigation).WithMany(p => p.InstructionsVersions)
                .HasForeignKey(d => d.CodeBain)
                .HasConstraintName("FK_InstructionsVersions_Bains");
        });

        modelBuilder.Entity<LignesDemandesInterventionsAnalysis>(entity =>
        {
            entity.ToTable("Lignes_Demandes_Interventions_Analyses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ajout)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("ajout");
            entity.Property(e => e.CodeArticle)
                .HasMaxLength(10)
                .HasColumnName("codeArticle");
            entity.Property(e => e.CodeBain)
                .HasMaxLength(40)
                .HasColumnName("codeBain");
            entity.Property(e => e.Coefficient1)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("coefficient1");
            entity.Property(e => e.Coefficient2)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("coefficient2");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.EmplacementBain)
                .HasMaxLength(40)
                .HasColumnName("emplacementBain");
            entity.Property(e => e.Etat)
                .HasMaxLength(20)
                .HasColumnName("etat");
            entity.Property(e => e.IdEnteteDemandeInterventionAnlayse).HasColumnName("idEnteteDemandeInterventionAnlayse");
            entity.Property(e => e.Mesure)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("mesure");
            entity.Property(e => e.UniteAjout)
                .HasMaxLength(10)
                .HasColumnName("uniteAjout");
            entity.Property(e => e.UniteMesure)
                .HasMaxLength(10)
                .HasColumnName("uniteMesure");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");
            entity.Property(e => e.VolumePreleve)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("volumePreleve");

            entity.HasOne(d => d.IdEnteteDemandeInterventionAnlayseNavigation).WithMany(p => p.LignesDemandesInterventionsAnalyses)
                .HasForeignKey(d => d.IdEnteteDemandeInterventionAnlayse)
                .HasConstraintName("FK_LignesDemandesInterventionsAnalyses_EntetesDemandesInterventionsAnalyses");
        });

        modelBuilder.Entity<LignesDemandesInterventionsHull>(entity =>
        {
            entity.ToTable("Lignes_Demandes_Interventions_Hull");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodeBain)
                .HasMaxLength(40)
                .HasColumnName("codeBain");
            entity.Property(e => e.CodeProduit)
                .HasMaxLength(10)
                .HasColumnName("codeProduit");
            entity.Property(e => e.Coefficient)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("coefficient");
            entity.Property(e => e.Consomation)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("consomation");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr)
                .IsUnicode(false)
                .HasColumnName("creUsr");
            entity.Property(e => e.EmplacementBain)
                .HasMaxLength(40)
                .HasColumnName("emplacementBain");
            entity.Property(e => e.Etat)
                .HasMaxLength(20)
                .HasColumnName("etat");
            entity.Property(e => e.EtatFinal)
                .HasMaxLength(40)
                .HasColumnName("etatFinal");
            entity.Property(e => e.EtatInitial)
                .HasMaxLength(50)
                .HasColumnName("etatInitial");
            entity.Property(e => e.IdEnteteDemandeInterventionHull).HasColumnName("idEnteteDemandeInterventionHull");
            entity.Property(e => e.Unite)
                .HasMaxLength(50)
                .HasColumnName("unite");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr)
                .IsUnicode(false)
                .HasColumnName("updUsr");

            entity.HasOne(d => d.IdEnteteDemandeInterventionHullNavigation).WithMany(p => p.LignesDemandesInterventionsHulls)
                .HasForeignKey(d => d.IdEnteteDemandeInterventionHull)
                .HasConstraintName("FK_LignesDemandesInterventionsHull_EntetesDemandesInterventionsHull");
        });

        modelBuilder.Entity<LignesMouvementsStock>(entity =>
        {
            entity.ToTable("Lignes_Mouvements_Stock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodeArticle)
                .HasMaxLength(10)
                .HasColumnName("codeArticle");
            entity.Property(e => e.CodeEmplacementDestination).HasColumnName("codeEmplacementDestination");
            entity.Property(e => e.CodeEmplacementOrigine).HasColumnName("codeEmplacementOrigine");
            entity.Property(e => e.CodeEnteteMouvementStock)
                .HasMaxLength(20)
                .HasColumnName("codeEnteteMouvementStock");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.DesignationArticle)
                .HasMaxLength(50)
                .HasColumnName("designationArticle");
            entity.Property(e => e.Quantite).HasColumnName("quantite");
            entity.Property(e => e.Unite)
                .HasMaxLength(10)
                .HasColumnName("unite");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeArticleNavigation).WithMany(p => p.LignesMouvementsStocks)
                .HasForeignKey(d => d.CodeArticle)
                .HasConstraintName("FK_LignesMouvementsStock_Articles");

            entity.HasOne(d => d.CodeEmplacementDestinationNavigation).WithMany(p => p.LignesMouvementsStockCodeEmplacementDestinationNavigations)
                .HasForeignKey(d => d.CodeEmplacementDestination)
                .HasConstraintName("FK_LignesMouvementsStock_Emplacements_Destination");

            entity.HasOne(d => d.CodeEmplacementOrigineNavigation).WithMany(p => p.LignesMouvementsStockCodeEmplacementOrigineNavigations)
                .HasForeignKey(d => d.CodeEmplacementOrigine)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LignesMouvementsStock_Emplacements");

            entity.HasOne(d => d.CodeEnteteMouvementStockNavigation).WithMany(p => p.LignesMouvementsStocks)
                .HasForeignKey(d => d.CodeEnteteMouvementStock)
                .HasConstraintName("FK_LignesMouvementsStock_EntetesMouvementsStock");
        });

        modelBuilder.Entity<ParametresChimique>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_Paramètres_Bains");

            entity.ToTable("Parametres_Chimiques");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Nom).HasColumnName("nom");
            entity.Property(e => e.TypeValeur)
                .HasMaxLength(20)
                .HasColumnName("typeValeur");
            entity.Property(e => e.Unite)
                .HasMaxLength(5)
                .HasColumnName("unite");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");
        });

        modelBuilder.Entity<ParametresVersion>(entity =>
        {
            entity.HasKey(e => new { e.IdGammeVersion, e.IdParametreChimique }).HasName("PK_affectation_parametres_versions");

            entity.ToTable("Parametres_Versions");

            entity.Property(e => e.IdGammeVersion).HasColumnName("idGammeVersion");
            entity.Property(e => e.IdParametreChimique)
                .HasMaxLength(10)
                .HasColumnName("idParametreChimique");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");
            entity.Property(e => e.Valeur)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valeur");
            entity.Property(e => e.ValeurMax)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valeurMax");
            entity.Property(e => e.ValeurMin)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("valeurMin");

            entity.HasOne(d => d.IdGammeVersionNavigation).WithMany(p => p.ParametresVersions)
                .HasForeignKey(d => d.IdGammeVersion)
                .HasConstraintName("FK_AffectationParametresVersions_Versions");

            entity.HasOne(d => d.IdParametreChimiqueNavigation).WithMany(p => p.ParametresVersions)
                .HasForeignKey(d => d.IdParametreChimique)
                .HasConstraintName("FK_Affectation_Parametre");
        });

        modelBuilder.Entity<PlansDemandesIntervention>(entity =>
        {
            entity.ToTable("Plans_Demandes_Interventions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CodeBain)
                .HasMaxLength(40)
                .HasColumnName("codeBain");
            entity.Property(e => e.CodePosteCharge)
                .HasMaxLength(50)
                .HasColumnName("codePosteCharge");
            entity.Property(e => e.Compteur).HasColumnName("compteur");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Frequence)
                .HasMaxLength(50)
                .HasColumnName("frequence");
            entity.Property(e => e.Intitule).HasColumnName("intitule");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");
        });

        modelBuilder.Entity<PostesDeCharge>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Postes_De_Charge");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CodeCentreCharge)
                .HasMaxLength(10)
                .HasColumnName("codeCentreCharge");
            entity.Property(e => e.CodeSite)
                .HasMaxLength(10)
                .HasColumnName("codeSite");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .HasColumnName("designation");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeCentreChargeNavigation).WithMany(p => p.PostesDeCharges)
                .HasForeignKey(d => d.CodeCentreCharge)
                .HasConstraintName("FK_PostesDeCharge_CentresDeCharge");

            entity.HasOne(d => d.CodeSiteNavigation).WithMany(p => p.PostesDeCharges)
                .HasForeignKey(d => d.CodeSite)
                .HasConstraintName("FK_PostesDeCharge_Sites");
        });

        modelBuilder.Entity<RatioArticle>(entity =>
        {
            entity.ToTable("Ratio_Article");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CodeArticle)
                .HasMaxLength(10)
                .HasColumnName("codeArticle");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");
            entity.Property(e => e.ValeurMesure)
                .HasColumnType("decimal(6, 3)")
                .HasColumnName("valeurMesure");
            entity.Property(e => e.ValeurRatio)
                .HasColumnType("decimal(6, 3)")
                .HasColumnName("valeurRatio");

            entity.HasOne(d => d.CodeArticleNavigation).WithMany(p => p.RatioArticles)
                .HasForeignKey(d => d.CodeArticle)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RatioArticle_Articles");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CodeSociete)
                .HasMaxLength(10)
                .HasColumnName("codeSociete");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .HasColumnName("designation");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeSocieteNavigation).WithMany(p => p.Sites)
                .HasForeignKey(d => d.CodeSociete)
                .HasConstraintName("FK_Sites_Societes");
        });

        modelBuilder.Entity<Societe>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .HasColumnName("designation");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.CodeArticle, e.CodeEmplacement, e.CodeDepot });

            entity.Property(e => e.CodeArticle)
                .HasMaxLength(10)
                .HasColumnName("codeArticle");
            entity.Property(e => e.CodeEmplacement).HasColumnName("codeEmplacement");
            entity.Property(e => e.CodeDepot)
                .HasMaxLength(10)
                .HasColumnName("codeDepot");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.Quantite).HasColumnName("quantite");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.CodeArticleNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.CodeArticle)
                .HasConstraintName("FK_Stocks_Articles");

            entity.HasOne(d => d.CodeDepotNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.CodeDepot)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stocks_Depots");

            entity.HasOne(d => d.CodeEmplacementNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.CodeEmplacement)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stocks_Emplacements");
        });

        modelBuilder.Entity<TablesDiversesDefinition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Tables_Diverses");

            entity.ToTable("Tables_Diverses_Definition");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.IdTableDependance).HasColumnName("idTableDependance");
            entity.Property(e => e.Intitule).HasColumnName("intitule");
            entity.Property(e => e.NomColonneAlpha1).HasColumnName("nomColonneAlpha1");
            entity.Property(e => e.NomColonneAlpha2).HasColumnName("nomColonneAlpha2");
            entity.Property(e => e.NomColonneNumerique1).HasColumnName("nomColonneNumerique1");
            entity.Property(e => e.NomColonneNumerique2).HasColumnName("nomColonneNumerique2");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");

            entity.HasOne(d => d.IdTableDependanceNavigation).WithMany(p => p.InverseIdTableDependanceNavigation)
                .HasForeignKey(d => d.IdTableDependance)
                .HasConstraintName("FK_Diverse_TableReference");
        });

        modelBuilder.Entity<TablesDiversesDonnee>(entity =>
        {
            entity.ToTable("Tables_Diverses_Donnees");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasColumnName("code");
            entity.Property(e => e.CreDat)
                .HasColumnType("datetime")
                .HasColumnName("creDat");
            entity.Property(e => e.CreUsr).HasColumnName("creUsr");
            entity.Property(e => e.IdTableDiverse).HasColumnName("idTableDiverse");
            entity.Property(e => e.IntituleCode).HasColumnName("intituleCode");
            entity.Property(e => e.UpdDat)
                .HasColumnType("datetime")
                .HasColumnName("updDat");
            entity.Property(e => e.UpdUsr).HasColumnName("updUsr");
            entity.Property(e => e.ValeurColonneAlpha1).HasColumnName("valeurColonneAlpha1");
            entity.Property(e => e.ValeurColonneAlpha2).HasColumnName("valeurColonneAlpha2");
            entity.Property(e => e.ValeurColonneDependance)
                .HasMaxLength(50)
                .HasColumnName("valeurColonneDependance");
            entity.Property(e => e.ValeurColonneNumerique1).HasColumnName("valeurColonneNumerique1");
            entity.Property(e => e.ValeurColonneNumerique2).HasColumnName("valeurColonneNumerique2");

            entity.HasOne(d => d.IdTableDiverseNavigation).WithMany(p => p.TablesDiversesDonnees)
                .HasForeignKey(d => d.IdTableDiverse)
                .HasConstraintName("FK_Donnees_Definition");
        });

        modelBuilder.Entity<BainReferenceAlternativeView>()
            .ToView("V_Bains_Reference_Alternatives")
            .HasNoKey();


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
