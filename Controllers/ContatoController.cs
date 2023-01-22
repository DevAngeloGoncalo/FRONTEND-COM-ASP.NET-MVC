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
    }
}