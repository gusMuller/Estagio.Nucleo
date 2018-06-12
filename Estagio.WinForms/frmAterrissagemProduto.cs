﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Estagio.Nucleo;
using Estagio.Nucleo.Repositorios;

namespace Estagio.WinForms
{
    public partial class frmAterrissagemProduto : frmBaseAterrissagem
    {
        public frmAterrissagemProduto()
        {
            InitializeComponent();
        }

        private void frmAterrissagemProduto_Load(object sender, EventArgs e)
        {
            Produto produto01 = new Produto();
            produto01.Id = 1;
            produto01.PrecoUnitario = 15.50m;
            produto01.QuantidadeMinimaEstoque = 10;
            produto01.Descricao = "Pão";


            Produto produto02 = new Produto();
            produto02.Id = 2;
            produto02.PrecoUnitario = 30;
            produto02.QuantidadeMinimaEstoque = 20;
            produto02.Descricao = "Batata";

            Cliente cliente01 = new Cliente();
            cliente01.Id = 1;
            cliente01.Nome = "Josivaldo";
            CPFCNPJ cPFCNPJCliente01 = new CPFCNPJ("447.685.060-03");
            cliente01.CPFCNPJ = cPFCNPJCliente01;

            RepositorioDeCliente.Instancia.Add(cliente01);

            RepositorioDoProduto.Instancia.Add(produto01);
            RepositorioDoProduto.Instancia.Add(produto02);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            bsDgvProdutos.DataSource = RepositorioDoProduto.Instancia.GetAll();
            bsDgvProdutos.ResetBindings(true);
        }

        protected override void CrieColunasELinhasDoDataGrid()
        {
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 100,
                DataPropertyName = nameof(Produto.Id),
                HeaderText = "ID",
                Name = nameof(Produto.Id)
            });

            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Width = 100,
                DataPropertyName = nameof(Produto.Descricao),
                HeaderText = "Descrição",
                Name = nameof(Produto.Descricao)
            });
        }

        protected override object selecionaProduto()
        {
            return bsDgvProdutos.Current;
        }

        

        protected override Form InicializeFrmComObjeto()
        {
            return new frmCadastrarOuEditarProduto((Produto)selecionaProduto());
        }

        protected override Form inicializeFrmNulo()
        {
            return new frmCadastrarOuEditarProduto(new Produto());
        }




    }
}