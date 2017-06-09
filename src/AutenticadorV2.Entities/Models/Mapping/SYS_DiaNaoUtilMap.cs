using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_DiaNaoUtilMap : EntityTypeConfiguration<SYS_DiaNaoUtil>
    {
        public SYS_DiaNaoUtilMap()
        {
            // Primary Key
            this.HasKey(t => t.dnu_id);

            // Properties
            this.Property(t => t.dnu_nome)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.dnu_descricao)
                .HasMaxLength(400);

            // Table & Column Mappings
            this.ToTable("SYS_DiaNaoUtil");
            this.Property(t => t.dnu_id).HasColumnName("dnu_id");
            this.Property(t => t.dnu_nome).HasColumnName("dnu_nome");
            this.Property(t => t.dnu_abrangencia).HasColumnName("dnu_abrangencia");
            this.Property(t => t.dnu_descricao).HasColumnName("dnu_descricao");
            this.Property(t => t.dnu_data).HasColumnName("dnu_data");
            this.Property(t => t.dnu_recorrencia).HasColumnName("dnu_recorrencia");
            this.Property(t => t.dnu_vigenciaInicio).HasColumnName("dnu_vigenciaInicio");
            this.Property(t => t.dnu_vigenciaFim).HasColumnName("dnu_vigenciaFim");
            this.Property(t => t.cid_id).HasColumnName("cid_id");
            this.Property(t => t.unf_id).HasColumnName("unf_id");
            this.Property(t => t.dnu_situacao).HasColumnName("dnu_situacao");
            this.Property(t => t.dnu_dataCriacao).HasColumnName("dnu_dataCriacao");
            this.Property(t => t.dnu_dataAlteracao).HasColumnName("dnu_dataAlteracao");

            // Relationships
            this.HasOptional(t => t.END_Cidade)
                .WithMany(t => t.SYS_DiaNaoUtil)
                .HasForeignKey(d => d.cid_id);
            this.HasOptional(t => t.END_UnidadeFederativa)
                .WithMany(t => t.SYS_DiaNaoUtil)
                .HasForeignKey(d => d.unf_id);

        }
    }
}
