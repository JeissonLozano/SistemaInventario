﻿using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class UnidadTrabajo :IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IBodegaRepositorio Bodega { get; private set; }
        public ICategoriaRepositorio Categoria { get; private set; }
        public IMarcaRepositorio Marca { get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Bodega = new BodegaRepositorio(_db); //Inicializamos
            Categoria = new CategoriaRepositorio(_db);
            Marca = new MarcaRepositorio(_db);
        }


        public void Guardar()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
