using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class CFG_UsuarioAPIMap : EntityTypeConfiguration<CFG_UsuarioAPI>
    {
        public CFG_UsuarioAPIMap()
        {
            // Primary Key
            this.HasKey(t => t.uap_id);

            // Properties
            this.Property(t => t.uap_username)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.uap_password)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("CFG_UsuarioAPI");
            this.Property(t => t.uap_id).HasColumnName("uap_id");
            this.Property(t => t.uap_username).HasColumnName("uap_username");
            this.Property(t => t.uap_password).HasColumnName("uap_password");
            this.Property(t => t.uap_situacao).HasColumnName("uap_situacao");
            this.Property(t => t.uap_dataCriacao).HasColumnName("uap_dataCriacao");
            this.Property(t => t.uap_dataAlteracao).HasColumnName("uap_dataAlteracao");
        }
    }
}
