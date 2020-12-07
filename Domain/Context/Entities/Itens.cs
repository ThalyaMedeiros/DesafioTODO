using Domain.Context.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Context.Entities
{
    public class Itens: Entity
    {
        public Itens(string descricao)
        {

            ValidarDescricao();

            Descricao = descricao;
        }

        public string Descricao { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public DateTime DataVencimento { get; private set; }

        public DateTime DataConclusao { get; private set; }

        public DateTime DataAtualizacao { get; private set; }

        public EStatusItens Status { get; private set; }

        public string ApplicationUserId { get; private set; }

        public void ValidarDescricao()
        {
            //Verificar se a descrição é nula
            if (string.IsNullOrEmpty(this.Descricao))
            {
                throw new Exception("Descrição não pode estar vazia."); // retornar erro/Exception               
            }
            return;
        }

        public void AtualizarItemConcluido()
        {
            if(Status == EStatusItens.Concluido)
            {
                throw new Exception("Não é possivel atualizar um item concluido.");
            }           
        }
        
    }
}
