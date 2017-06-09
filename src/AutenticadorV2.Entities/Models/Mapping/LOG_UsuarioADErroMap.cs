using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class LOG_UsuarioADErroMap : EntityTypeConfiguration<LOG_UsuarioADErro>
    {
        public LOG_UsuarioADErroMap()
        {
            // Primary Key
            this.HasKey(t => t.usa_id);

            // Properties
            this.Property(t => t.usa_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.use_descricaoErro)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("LOG_UsuarioADErro");
            this.Property(t => t.usa_id).HasColumnName("usa_id");
            this.Property(t => t.use_descricaoErro).HasColumnName("use_descricaoErro");

            // Relationships
            this.HasRequired(t => t.LOG_UsuarioAD)
                .WithOptional(t => t.LOG_UsuarioADErro);

        }
    }
}
