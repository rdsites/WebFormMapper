using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace WebTests
{
    public partial class _Default : System.Web.UI.Page
    {
        // Declara um mapper para tela
        private WebFormMapper.Mapper<DefaultViewModel> mapper;
        // Declara uma model única para a tela
        private DefaultViewModel viewModel;

        protected void Page_Init(object sender, EventArgs e)
        {
            CultureInfo provider = new CultureInfo("pt-BR");

            // Instancia a model
            viewModel = new DefaultViewModel();
            // Instancia o mapper
            mapper = new WebFormMapper.Mapper<DefaultViewModel>(viewModel);
            // Adiciona os controles que serão mapeados aos atributos da model
            mapper.AddMapping(txtTextBox, "Nome");
            mapper.AddMapping(txtNascimento, "DataNascimento");
            mapper.AddMapping(txtIdade, "Idade");
            mapper.AddMapping(rblRadioList, "Sexo");
            mapper.AddMapping(ddlEstadoCivil, "EstadoCivil");
            // Adiciona formatações para cada controle
            mapper.AddFormatting(txtNascimento, provider);
            mapper.AddFormatting(txtIdade, provider);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                viewModel.Nome = "Teste de Nome";
                viewModel.DataNascimento = DateTime.Now.AddDays(-1);
                //viewModel.Idade = 328799.56M;
                viewModel.Sexo = 1;
                viewModel.EstadoCivil = "S";
                // Chamando esse método, os controles serão populados com os valores dos atributos mapeados da model para o controle
                mapper.MapToControls();
            }
        }

        protected void Testar(object sender, EventArgs e)
        {
            // Chamando esse método, os valores dos controles serão populados de volta à model
            viewModel = mapper.MapToModel();
            viewModel.Nome = string.Format("Alterado {0:dd/MM/yyyy hh:mm:ss} --> {1}", viewModel.DataNascimento, viewModel.EstadoCivil);
            mapper.MapToControls();
        }

        // View Model criada para exemplo desta tela
        class DefaultViewModel
        {
            public string Nome { get; set; }
            public decimal Idade { get; set; }
            public DateTime DataNascimento { get; set; }
            public int Sexo { get; set; }
            public string EstadoCivil { get; set; }
        }
    }
}
