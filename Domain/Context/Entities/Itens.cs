using Domain.Context.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Context.Entities
{
    public class Itens : Entity
    {
        public Itens(string descricao)
        {

            Descricao = descricao;
        }

        public string Descricao { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public DateTime DataVencimento { get; private set; }

        public DateTime DataConclusao { get; private set; }

        public DateTime DataAtualizacao { get; private set; }

        public EStatusItens Status { get; private set; }

        public string UsuarioId { get; private set; }

        private void ValidarDescricao()
        {
            //Verificar se a descrição é nula
            if (string.IsNullOrEmpty(this.Descricao))
            {
                throw new Exception("Descrição não pode estar vazia."); // retornar erro/Exception               
            }
            return;
        }

        private void AtualizarItemConcluido()
        {
            if (Status == EStatusItens.Concluido)
            {
                throw new Exception("Não é possivel atualizar um item concluido.");
            }
        }

        private void ValidarDataVencimento(DateTime dataVencimento)
        {
            //DataVencimento
            if(dataVencimento == DateTime.MinValue)
            {
                throw new Exception("Insira uma data de vencimento valida.");
            }
        }

        private void ValidarUsuario(string usuarioId)
        {
            UsuarioId = usuarioId;
            if (string.IsNullOrEmpty(UsuarioId))
            {
                throw new Exception("Usuario não identificado, verifique o seu login"); // retornar erro/Exception               
            }
            return;
        }

        public void CriarItem(string usuarioId, DateTime dataVencimento)
        {
            ValidarDescricao();
           
            DataCriacao = DateTime.Now;

            Status = EStatusItens.Criado;

            ValidarDataVencimento(dataVencimento);

            ValidarUsuario(usuarioId);

            VerificarDataVencimento(dataVencimento);

            DataVencimento = dataVencimento;

        }

        public void ConcluirItem()
        {
            Status = EStatusItens.Concluido;
            DataConclusao = DateTime.Now;
        }

        public void EditarItem(string descricao, DateTime dataVencimento)
        {
            AtualizarItemConcluido();

            Descricao = descricao;
            DataVencimento = dataVencimento != DateTime.MinValue ? dataVencimento : DataVencimento;
            Status = EStatusItens.Atualizado;
            DataAtualizacao = DateTime.Now;

            VerificarDataVencimento(DataVencimento);
        }

        private void VerificarDataVencimento(DateTime dataVencimento)
        {
            if(dataVencimento != DateTime.MinValue && dataVencimento < DateTime.Today)
            {
                this.DataVencimento = dataVencimento;
                this.Status = EStatusItens.Atrasado;
            }
        }

        public void VerificarListaDataVencimento(Itens item)
        {
            if (item.DataVencimento != DateTime.MinValue && item.DataVencimento < DateTime.Today)
            {
                this.Status = EStatusItens.Atrasado;
            }
        }

    }
}
