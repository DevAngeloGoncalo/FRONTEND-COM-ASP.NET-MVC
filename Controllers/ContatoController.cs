using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Context;
using ProjetoMVC.Models;

namespace ProjetoMVC.Controllers
{
    public class ContatoController : Controller
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //Transformar em uma lista
            var contatos = _context.Contatos.ToList();
            return View(contatos);  
        } 

        //Possui dois métodos criar, o vazio é para ser o primeiro a ser aberto
        public IActionResult Criar()
        {
            return View();
        }

        //Será chamado caso seja acionado o botão criar
        [HttpPost]
        public IActionResult Criar(Contato contato)
        {
            if (ModelState.IsValid)
            {
                _context.Contatos.Add(contato);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));  //Volta para index (Listagem)
            }
            else
            {
                return View(contato);
            }
        }

        //Será chamado caso seja acionado o botão Editar da lista de contatos
        public IActionResult Editar(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(contato);
            } 
        }

        //Será chamado caso seja acionado o botão Editar
        [HttpPost]
        public IActionResult Editar(Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(contato.Id);

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;
            
            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(contato);
            }    
        }

        public IActionResult Deletar(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(contato);
            }   
        }

        [HttpPost]
        public IActionResult Deletar(Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(contato.Id);
            
            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));  
        }
    }
}