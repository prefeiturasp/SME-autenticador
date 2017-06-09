using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_ParametroMap : EntityTypeConfiguration<SYS_Parametro>
    {
        public SYS_ParametroMap()
        {
            // Primary Key
            this.HasKey(t => t.par_id);

            // Properties
            this.Property(t => t.par_chave)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.par_valor)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.par_descricao)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SYS_Parametro");
            this.Property(t => t.par_id).HasColumnName("par_id");
            this.Property(t => t.par_chave).HasColumnName("par_chave");
            this.Property(t => t.par_valor).HasColumnName("par_valor");
            this.Property(t => t.par_descricao).HasColumnName("par_descricao");
            this.Property(t => t.par_situacao).HasColumnName("par_situacao");
            this.Property(t => t.par_vigenciaInicio).HasColumnName("par_vigenciaInicio");
            this.Property(t => t.par_vigenciaFim).HasColumnName("par_vigenciaFim");
            this.Property(t => t.par_dataCriacao).HasColumnName("par_dataCriacao");
            this.Property(t => t.par_dataAlteracao).HasColumnName("par_dataAlteracao");
            this.Property(t => t.par_obrigatorio).HasColumnName("par_obrigatorio");
        }
    }
}
