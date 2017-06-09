using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_ParametroGrupoPerfilMap : EntityTypeConfiguration<SYS_ParametroGrupoPerfil>
    {
        public SYS_ParametroGrupoPerfilMap()
        {
            // Primary Key
            this.HasKey(t => t.pgs_id);

            // Properties
            this.Property(t => t.pgs_chave)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SYS_ParametroGrupoPerfil");
            this.Property(t => t.pgs_id).HasColumnName("pgs_id");
            this.Property(t => t.pgs_chave).HasColumnName("pgs_chave");
            this.Property(t => t.gru_id).HasColumnName("gru_id");
            this.Property(t => t.pgs_situacao).HasColumnName("pgs_situacao");
            this.Property(t => t.pgs_dataCriacao).HasColumnName("pgs_dataCriacao");
            this.Property(t => t.pgs_dataAlteracao).HasColumnName("pgs_dataAlteracao");

            // Relationships
            this.HasRequired(t => t.SYS_Grupo)
                .WithMany(t => t.SYS_ParametroGrupoPerfil)
                .HasForeignKey(d => d.gru_id);

        }
    }
}
