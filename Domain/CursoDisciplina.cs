using System;

namespace Exemplo01.Domain
{
    public class CursoDisciplina
    {
        public Guid Id { get; set; }
        public String Descricao { get; set; }
        public Guid CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}