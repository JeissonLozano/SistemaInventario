﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Categoria categoria = new Categoria();
            if (id == null)
            {
                //Esto es para crear nuevo Registro
                return View(categoria);
            }
            // Esto es para Actualizar
            categoria = _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());

            if(categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Categoria categoria)
        {
            if(ModelState.IsValid)
            {
                if(categoria.id == 0)
                {
                    _unidadTrabajo.Categoria.Agregar(categoria);
                }
                else
                {
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                }
                _unidadTrabajo.Guardar();

                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var categoriaDb = _unidadTrabajo.Categoria.Obtener(id);
            if(categoriaDb == null)
            {
                return Json(new { success = false, message = "error al Borrar" });
            }
            _unidadTrabajo.Categoria.Remover(categoriaDb);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria Borrada exitosamente" });
        }
        #endregion

    }
}
