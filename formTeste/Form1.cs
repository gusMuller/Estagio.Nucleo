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
using Estagio.Nucleo.Movimentacao;

namespace formTeste
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
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

            cmbSelecionaProduto.Items.AddRange(RepositorioDoProduto.Instancia.GetAll().ToArray());


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CPFCNPJ cPFCNPJ = new CPFCNPJ(tbCpfCnpj.Text);
                tbCpfCnpjFormatado.Text = cPFCNPJ.ObtenhaCPFCNPJFormatado();
            }
            catch (ApplicationException)
            {
                if (String.IsNullOrWhiteSpace(tbCpfCnpj.Text))
                {
                    MessageBox.Show("Campo vazio!\nPreencha com algum CPF / CNPJ válido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbCpfCnpjFormatado.Text = String.Empty;
                }
                else
                {
                    MessageBox.Show("CPF / CNPJ Inválido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbCpfCnpjFormatado.Text = String.Empty;
                }
            }
            tbCpfCnpj.Focus();
        }



        private void tbCpfCnpj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Produto produtoUpdate = new Produto();
                produtoUpdate.Id = ((Produto)cmbSelecionaProduto.SelectedItem).Id;
                produtoUpdate.Descricao = cmbSelecionaProduto.Text;
                produtoUpdate.PrecoUnitario = Convert.ToDecimal(tbPrecoUni.Text);
                produtoUpdate.QuantidadeMinimaEstoque = Convert.ToInt32(tbQuantidade.Text);
                RepositorioDoProduto.Instancia.Update(produtoUpdate);
                MessageBox.Show("Produto atualizado!");
            }
            catch
            {
                MessageBox.Show("Produto não existe!");
            }
            LimpeOsCamposTb();
            cmbSelecionaProduto.Items.AddRange(RepositorioDoProduto.Instancia.GetAll().ToArray());
        }


        private void cmbSelecionaProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selecionado = (Produto)cmbSelecionaProduto.SelectedItem;
            tbPrecoUni.Text = selecionado.PrecoUnitario.ToString();
            tbQuantidade.Text = selecionado.QuantidadeMinimaEstoque.ToString();
        }
        private void LimpeOsCamposTb()
        {
            cmbSelecionaProduto.Text = String.Empty;
            tbPrecoUni.Text = String.Empty;
            tbQuantidade.Text = String.Empty;
            cmbSelecionaProduto.Items.Clear();
        }

        private void btVenda_Click(object sender, EventArgs e)
        {
            Produto produtoVenda = new Produto();
            produtoVenda.Id = ((Produto)cmbSelecionaProduto.SelectedItem).Id;
            produtoVenda.Descricao = cmbSelecionaProduto.Text;
            produtoVenda.PrecoUnitario = Convert.ToDecimal(tbPrecoUni.Text);
            produtoVenda.QuantidadeMinimaEstoque = Convert.ToInt32(tbQuantidade.Text);

        }

       
       
        private void tbCpfCnpjFormatado_KeyPress_1(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }
    }
}