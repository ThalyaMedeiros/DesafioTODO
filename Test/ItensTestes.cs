using Domain.Context.Entities;
using Domain.Context.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class ItensTestes
    {
        private Usuarios usuarioCliente;
        private Itens item1;

        public ItensTestes()
        {
            usuarioCliente = new Usuarios("Thalya", "thalya@gmail.com", "@Desafio123");
            item1 = new Itens("Item 1");
        }

        [TestMethod]
        public void CriarItem()
        {
            item1.CriarItem(usuarioCliente.Id, DateTime.Today.AddDays(5));
            Assert.AreEqual(true, item1.Status == EStatusItens.Criado);
        }

        [TestMethod]
        public void EditarItem()
        {
            item1.EditarItem("Editar descrição do Item", DateTime.Today.AddDays(10));
            Assert.AreEqual(true, item1.Status == EStatusItens.Atualizado);
        }

        [TestMethod]
        public void ConcluirItem()
        {
            item1.ConcluirItem();
            Assert.AreEqual(true, item1.Status == EStatusItens.Concluido);
        }
        
    }
}
