using Ateliex.Cadastro.Modelos;
using Ateliex.Decisoes.Comerciais;
using Microsoft.EntityFrameworkCore;

namespace Ateliex
{
    public class AteliexDbContext : DbContext
    {
        public DbSet<Modelo> Modelos { get; set; }

        public DbSet<PlanoComercial> PlanosComerciais { get; set; }

        public AteliexDbContext()
        {

        }

        public AteliexDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder
            //    //.UseLazyLoadingProxies()
            //    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Comum.

            ////modelBuilder.Entity<Pessoa>()
            ////    .OwnsOne(p => p.Endereco);

            ////modelBuilder.Entity<PessoaJuridica>()
            ////    .OwnsOne(p => p.InformacoesBancarias);

            //modelBuilder.Entity<Pessoa>()
            //    .OwnsMany(p => p.ContatosDeTelefone);

            //modelBuilder.Entity<ContatoDeTelefone>()
            //    .HasKey(p => p.Id);

            //modelBuilder.Entity<ContatoDeTelefone>()
            //    .OwnsOne(p => p.Telefone);

            //modelBuilder.Entity<Pessoa>()
            //    .OwnsMany(p => p.ContatosDeEmail);

            //modelBuilder.Entity<ContatoDeEmail>()
            //    .HasKey(p => p.Id);

            //modelBuilder.Entity<ContatoDeEmail>()
            //    .OwnsOne(p => p.Email);

            //modelBuilder.Entity<Unidade>()
            //    .HasKey(p => p.Sigla);

            //// Cadastro - Unidades.

            //// Cadastro - Cores.

            ////modelBuilder.Entity<CorInterna>()
            ////    .HasKey(p => p.Codigo);

            //// Cadastro - Materiais - Fabricantes.

            ////modelBuilder.Entity<CorDeFabricante>()
            ////    .HasKey(p => new { p.FabricanteId, p.ComponenteId, p.CatalogoNome, p.Codigo });

            ////modelBuilder.Entity<CorDeFabricante>()
            ////    .Property(p => p.CustoPadrao)
            ////    .HasColumnType("DECIMAL (18, 2)");

            ////modelBuilder.Entity<CorDeFabricante>()
            ////    .HasOne(p => p.CorDeUsoInterno)
            ////    .WithMany()
            ////    .HasForeignKey(p => p.CorDeUsoInternoCodigo);

            ////modelBuilder.Entity<CorDeFabricante>()
            ////    .HasOne(p => p.Catalogo)
            ////    .WithMany(p => p.Cores)
            ////    .HasForeignKey(p => new { p.FabricanteId, p.ComponenteId, p.CatalogoNome })
            ////    .IsRequired();

            //modelBuilder.Entity<FabricacaoDeComponente>()
            //    .HasKey(p => new { p.FabricanteId, p.ComponenteId });

            //modelBuilder.Entity<FabricacaoDeComponente>()
            //    .HasOne(p => p.Componente)
            //    .WithMany(p => p.Fabricantes)
            //    .HasForeignKey(p => p.ComponenteId);

            //modelBuilder.Entity<FabricacaoDeComponente>()
            //    .HasOne(p => p.Fabricante)
            //    .WithMany(p => p.ComponentesFabricados)
            //    .HasForeignKey(p => p.FabricanteId);

            ////modelBuilder.Entity<Catalogo>()
            ////    .HasKey(p => new { p.FabricanteId, p.ComponenteId, p.Nome });

            ////modelBuilder.Entity<Catalogo>()
            ////    .HasMany(p => p.Cores)
            ////    .WithOne(p => p.Catalogo)
            ////    .HasForeignKey(p => new { p.FabricanteId, p.ComponenteId, p.CatalogoNome })
            ////    .IsRequired();

            ////modelBuilder.Entity<DisponibilidadeDeEmbalagem>()
            ////    .HasKey(p => new { p.FabricanteId, p.ComponenteId, p.CatalogoNome, p.EmbalagemNome });

            ////modelBuilder.Entity<DisponibilidadeDeEmbalagem>()
            ////    .OwnsOne(p => p.Embalagem);

            //// Cadastro - Materiais - Fornecedores.

            //modelBuilder.Entity<FornecimentoDeMaterial>()
            //    .HasKey(p => new { p.FornecedorId, p.MaterialId });

            ////modelBuilder.Entity<FornecimentoDeMaterial>()
            ////    .OwnsOne(p => p.TamanhoMinimoPorPedido);

            //modelBuilder.Entity<FornecimentoDeMaterial>()
            //    .Property(p => p.UltimoPreco)
            //    .HasColumnType("DECIMAL (18, 2)");

            //modelBuilder.Entity<DisponibilidadeDeMeioDePagamento>()
            //    .HasKey(p => new { p.FornecedorId, p.MeioDePagamentoId });

            //// Cadastro - Materiais - Fornecedores.

            ////modelBuilder.Entity<Material>()
            ////    .OwnsOne(p => p.Embalagem);

            //modelBuilder.Entity<Material>()
            //    .Property(p => p.CustoPadrao)
            //    .HasColumnType("DECIMAL (18, 2)");

            ////

            //modelBuilder.Entity<AplicacaoDeInvestimento>()
            //    .HasKey(p => new { p.ModeloCodigo, p.InvestimentoId });

            modelBuilder.Entity<Modelo>()
                .HasKey(p => p.Codigo);

            modelBuilder.Entity<Modelo>()
                .Property(p => p.Codigo)
                .ValueGeneratedNever();

            modelBuilder.Entity<Recurso>()
                .HasKey(p => p.Descricao);

            //modelBuilder.Entity<TamanhoDeModelo>()
            //    .HasKey(p => p.Sigla);

            //modelBuilder.Entity<NecessidadeDeFerramentaDeProducao>()
            //    .HasKey(p => new { p.EtapaDeProducaoId, p.FerramentaId });

            //modelBuilder.Entity<NecessidadeDeMaterial>()
            //    .HasKey(p => new { p.ModeloCodigo, p.MaterialId });

            //modelBuilder.Entity<NecessidadeDeTipoDeRecurso>()
            //    .HasKey(p => new { p.EtapaId, p.TipoDeRecursoId });

            modelBuilder.Entity<PlanoComercial>()
                .HasKey(p => p.Codigo);

            modelBuilder.Entity<PlanoComercial>()
                .Property(p => p.Codigo)
                .ValueGeneratedNever();

            //modelBuilder.Entity<ItemDePlanoComercial>()
            //    .OwnsOne(p => p.CustoDeProducao);

            modelBuilder.Entity<Recurso>()
                .HasKey(p => new { p.ModeloCodigo, p.Descricao });

            modelBuilder.Entity<Custo>()
                .HasKey(p => new { p.PlanoComercialCodigo, p.Descricao });

            modelBuilder.Entity<ItemDePlanoComercial>()
                .HasKey(p => new { p.PlanoComercialCodigo, p.Id });

            //


            // Seed.


            modelBuilder.Entity<Modelo>().HasData(
                new Modelo { Codigo = "TM01", Nome = "Tati Model 01" },
                new Modelo { Codigo = "TM02", Nome = "Tati Model 02" },
                new Modelo { Codigo = "TM03", Nome = "Tati Model 03" },
                new Modelo { Codigo = "TM04", Nome = "Tati Model 04" },
                new Modelo { Codigo = "TM05", Nome = "Tati Model 05" },
                new Modelo { Codigo = "TM06", Nome = "Tati Model 06" },
                new Modelo { Codigo = "TM07", Nome = "Tati Model 07" },
                new Modelo { Codigo = "TM08", Nome = "Tati Model 08" },
                new Modelo { Codigo = "TM09", Nome = "Tati Model 09" },
                new Modelo { Codigo = "TM10", Nome = "Tati Model 10" }
            );

            modelBuilder.Entity<Recurso>().HasData(
                new Recurso { ModeloCodigo = "TM01", Tipo = TipoDeRecurso.Material, Descricao = "Tecido", Custo = 20, Unidades = 2 },
                new Recurso { ModeloCodigo = "TM01", Tipo = TipoDeRecurso.Material, Descricao = "Linha", Custo = 4, Unidades = 20 },
                new Recurso { ModeloCodigo = "TM01", Tipo = TipoDeRecurso.Material, Descricao = "Outros", Custo = 5, Unidades = 1 },
                new Recurso { ModeloCodigo = "TM01", Tipo = TipoDeRecurso.Transporte, Descricao = "Transporte", Custo = 100, Unidades = 50 },
                new Recurso { ModeloCodigo = "TM01", Tipo = TipoDeRecurso.Humano, Descricao = "Costureira", Custo = 5, Unidades = 1 }
            );

            modelBuilder.Entity<PlanoComercial>().HasData(
                new PlanoComercial { Codigo = "PC01.A", Nome = "Normal", RendaBrutaMensal = 6000 },
                new PlanoComercial { Codigo = "PC01.B", Nome = "Moderado" },
                new PlanoComercial { Codigo = "PC01.C", Nome = "Ousado" }
            );

            modelBuilder.Entity<Custo>().HasData(
                new Custo { PlanoComercialCodigo = "PC01.A", Tipo = TipoDeCusto.Fixo, Descricao = "Prolabore", Valor = 1000 },
                new Custo { PlanoComercialCodigo = "PC01.A", Tipo = TipoDeCusto.Fixo, Descricao = "Aluguel", Valor = 900 },
                new Custo { PlanoComercialCodigo = "PC01.A", Tipo = TipoDeCusto.Variavel, Descricao = "Cartão", Percentual = 10 },
                new Custo { PlanoComercialCodigo = "PC01.A", Tipo = TipoDeCusto.Variavel, Descricao = "Comissão", Percentual = 10 },
                new Custo { PlanoComercialCodigo = "PC01.A", Tipo = TipoDeCusto.Variavel, Descricao = "Perda", Percentual = 2 }
            );

            modelBuilder.Entity<ItemDePlanoComercial>().HasData(
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.A", Id = 1, ModeloCodigo = "TM01", MargemPercentual = 1.93m },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.A", Id = 2, ModeloCodigo = "TM02" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.A", Id = 3, ModeloCodigo = "TM03" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.A", Id = 10, ModeloCodigo = "TM10" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.B", Id = 1, ModeloCodigo = "TM01" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.B", Id = 2, ModeloCodigo = "TM02" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.B", Id = 3, ModeloCodigo = "TM03" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.B", Id = 4, ModeloCodigo = "TM04" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.B", Id = 5, ModeloCodigo = "TM05" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.B", Id = 6, ModeloCodigo = "TM06" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.B", Id = 7, ModeloCodigo = "TM07" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.B", Id = 8, ModeloCodigo = "TM08" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.B", Id = 9, ModeloCodigo = "TM09" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.B", Id = 10, ModeloCodigo = "TM10" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.C", Id = 5, ModeloCodigo = "TM05" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.C", Id = 6, ModeloCodigo = "TM06" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.C", Id = 7, ModeloCodigo = "TM07" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.C", Id = 8, ModeloCodigo = "TM08" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.C", Id = 9, ModeloCodigo = "TM09" },
                new ItemDePlanoComercial { PlanoComercialCodigo = "PC01.C", Id = 10, ModeloCodigo = "TM10" }
           );
        }
    }
}
