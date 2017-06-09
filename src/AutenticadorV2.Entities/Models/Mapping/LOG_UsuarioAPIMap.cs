using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class LOG_UsuarioAPIMap : EntityTypeConfiguration<LOG_UsuarioAPI>
    {
        public LOG_UsuarioAPIMap()
        {
            // Primary Key
            this.HasKey(t => t.lua_id);

            // Properties
            // Table & Column Mappings
            this.ToTable("LOG_UsuarioAPI");
            this.Property(t => t.lua_id).HasColumnName("lua_id");
            this.Property(t => t.usu_id).HasColumnName("usu_id");
            this.Property(t => t.uap_id).HasColumnName("uap_id");
            this.Property(t => t.lua_acao).HasColumnName("lua_acao");
            this.Property(t => t.lua_dataHora).HasColumnName("lua_dataHora");

            // Relationships
            this.HasRequired(t => t.CFG_UsuarioAPI)
                .WithMany(t => t.LOG_UsuarioAPI)
                .HasForeignKey(d => d.uap_id);
            this.HasRequired(t => t.SYS_Usuario)
                .WithMany(t => t.LOG_UsuarioAPI)
                .HasForeignKey(d => d.usu_id);

        }
    }
}
