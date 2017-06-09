using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class VW_PES_PessoaMap : EntityTypeConfiguration<VW_PES_Pessoa>
    {
        public VW_PES_PessoaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.pes_id, t.pes_nome });

            // Properties
            this.Property(t => t.pes_nome)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("VW_PES_Pessoa");
            this.Property(t => t.pes_id).HasColumnName("pes_id");
            this.Property(t => t.pes_nome).HasColumnName("pes_nome");
            this.Property(t => t.pes_dataNascimento).HasColumnName("pes_dataNascimento");
        }
    }
}
