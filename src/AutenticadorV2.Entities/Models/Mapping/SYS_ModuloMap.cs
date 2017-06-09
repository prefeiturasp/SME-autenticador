using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_ModuloMap : EntityTypeConfiguration<SYS_Modulo>
    {
        public SYS_ModuloMap()
        {
            // Primary Key
            this.HasKey(t => new { t.sis_id, t.mod_id });

            // Properties
            this.Property(t => t.sis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.mod_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.mod_nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SYS_Modulo");
            this.Property(t => t.sis_id).HasColumnName("sis_id");
            this.Property(t => t.mod_id).HasColumnName("mod_id");
            this.Property(t => t.mod_nome).HasColumnName("mod_nome");
            this.Property(t => t.mod_descricao).HasColumnName("mod_descricao");
            this.Property(t => t.mod_idPai).HasColumnName("mod_idPai");
            this.Property(t => t.mod_auditoria).HasColumnName("mod_auditoria");
            this.Property(t => t.mod_situacao).HasColumnName("mod_situacao");
            this.Property(t => t.mod_dataCriacao).HasColumnName("mod_dataCriacao");
            this.Property(t => t.mod_dataAlteracao).HasColumnName("mod_dataAlteracao");

            // Relationships
            this.HasRequired(t => t.SYS_Sistema)
                .WithMany(t => t.SYS_Modulo)
                .HasForeignKey(d => d.sis_id);

        }
    }
}
