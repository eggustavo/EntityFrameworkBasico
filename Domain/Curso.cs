using System;
using System.Collections.Generic;

namespace Exemplo01.Domain
{
    public class Curso
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public List<CursoDisciplina> CursoDisciplinas { get; set; }
    }
}