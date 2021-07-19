using eShopEnterprise.Catalogo.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShopEnterprise.Catalogo.API.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(p => p.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,3)");

            builder.ToTable("Produtos");
        }
    }
}
