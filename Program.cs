using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exemplo01.Context;
using Exemplo01.Domain;

namespace Exemplo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var opcao = string.Empty;

            while (opcao != "99")
            {
                Console.Clear();
                Console.WriteLine("Menu");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("1 - Listar os Cursos");
                Console.WriteLine("2 - Criar um Curso");
                Console.WriteLine("3 - Alterar um Curso");
                Console.WriteLine("4 - Excluir um Curso");
                Console.WriteLine("5 - Criar um Curso com Disciplinas");
                Console.WriteLine("6 - Listar os Cursos com Disciplinas sem Include");
                Console.WriteLine("7 - Listar os Cursos com Disciplinas com Include");
                Console.WriteLine("8 - Listar Curso por Id");
                Console.WriteLine("9 - Listar Curso em Ordem Crescende por Descrição");
                Console.WriteLine("10 - Listar Curso em Ordem Decrescende por Descrição");
                Console.WriteLine("99 - Sair");
                Console.WriteLine("------------------------------------------");
                Console.Write("Escolha uma opção: ");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        {
                            ListaCurso();
                            Console.ReadKey();
                            break;
                        }
                    case "2":
                        {
                            CriarCurso();
                            Console.WriteLine("Curso Criado com Sucesso");
                            Console.ReadKey();
                            break;
                        }
                    case "3":
                        {
                            AlterarCurso();
                            Console.WriteLine("Curso Alterado com Sucesso");
                            Console.ReadKey();
                            break;
                        }
                    case "4":
                        {
                            ExcluirCurso();
                            Console.WriteLine("Curso Excluído com Sucesso");
                            Console.ReadKey();
                            break;
                        }
                    case "5":
                        {
                            CriarCursoDisciplina();
                            Console.WriteLine("Curso com Disciplina Criado com Sucesso");
                            Console.ReadKey();
                            break;
                        }
                    case "6":
                        {
                            ListaCursoDisciplinaSemInclude();
                            Console.ReadKey();
                            break;
                        }
                    case "7":
                        {
                            ListaCursoDisciplinaComInclude();
                            Console.ReadKey();
                            break;
                        }
                    case "8":
                        {
                            ListaCursoPorId();
                            Console.ReadKey();
                            break;
                        }
                    case "9":
                        {
                            ListaCursoOrdemCrescente();
                            Console.ReadKey();
                            break;
                        }
                    case "10":
                        {
                            ListaCursoOrdemDecrescente();
                            Console.ReadKey();
                            break;
                        }
                }
            }
        }

        public static void ListaCurso()
        {
            using (var context = new ExemploContext())
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Listagem de Cursos");
                Console.WriteLine("------------------------------------------");
                foreach (var curso in context.CursoSet.ToList())
                {
                    Console.WriteLine($"Id: {curso.Id}");
                    Console.WriteLine($"Descrição: {curso.Descricao}");
                    Console.WriteLine("------------------------------------------");
                }
            }
        }

        public static void CriarCurso()
        {
            using (var context = new ExemploContext())
            {
                var curso = new Curso()
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Curso Exemplo"
                };

                context.CursoSet.Add(curso);
                context.SaveChanges();
            }
        }

        public static void AlterarCurso()
        {
            var curso = new Curso();

            using (var context = new ExemploContext())
            {
                Console.Write("Informe o Id do Curso: ");
                curso.Id = Guid.Parse(Console.ReadLine());
                Console.Write("Infore a Descrição do Curso: ");
                curso.Descricao = Console.ReadLine();

                context.CursoSet.AddOrUpdate(curso);
                context.SaveChanges();
            }
        }

        public static void ExcluirCurso()
        {
            var curso = new Curso();

            using (var context = new ExemploContext())
            {
                Console.Write("Informe o Id do Curso: ");
                curso = context.CursoSet.Find(Guid.Parse(Console.ReadLine()));
                context.CursoSet.Remove(curso);
                context.SaveChanges();
            }
        }

        public static void CriarCursoDisciplina()
        {
            using (var context = new ExemploContext())
            {
                var curso = new Curso()
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Curso Exemplo",
                    CursoDisciplinas = new List<CursoDisciplina>()
                };

                var cursoDisciplica01 = new CursoDisciplina()
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Disciplina A"
                };

                var cursoDisciplica02 = new CursoDisciplina()
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Disciplina B"
                };


                curso.CursoDisciplinas.Add(cursoDisciplica01);
                curso.CursoDisciplinas.Add(cursoDisciplica02);

                context.CursoSet.Add(curso);
                context.SaveChanges();
            }
        }

        public static void ListaCursoDisciplinaSemInclude()
        {
            using (var context = new ExemploContext())
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Listagem de Cursos");
                Console.WriteLine("------------------------------------------");
                foreach (var curso in context.CursoSet.ToList())
                {
                    Console.WriteLine($"Id: {curso.Id}");
                    Console.WriteLine($"Descrição: {curso.Descricao}");
                    Console.WriteLine("------------------------------------------");

                    if (curso.CursoDisciplinas ==  null)
                        continue;

                    Console.WriteLine("  Disciplinas do Curso");
                    Console.WriteLine("  ------------------------------------------");

                    foreach (var cursoCursoDisciplina in curso.CursoDisciplinas)
                    {
                        Console.WriteLine($"  Id Disciplina: {cursoCursoDisciplina.Id}");
                        Console.WriteLine($"  Descrição Disciplina: {cursoCursoDisciplina.Descricao}");
                        Console.WriteLine("  ------------------------------------------");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }

        public static void ListaCursoDisciplinaComInclude()
        {
            using (var context = new ExemploContext())
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Listagem de Cursos");
                Console.WriteLine("------------------------------------------");
                foreach (var curso in context.CursoSet.Include("CursoDisciplinas").ToList())
                {
                    Console.WriteLine($"Id: {curso.Id}");
                    Console.WriteLine($"Descrição: {curso.Descricao}");
                    Console.WriteLine("------------------------------------------");

                    Console.WriteLine("  Disciplinas do Curso");
                    Console.WriteLine("  ------------------------------------------");

                    foreach (var cursoCursoDisciplina in curso.CursoDisciplinas)
                    {
                        Console.WriteLine($"  Id Disciplina: {cursoCursoDisciplina.Id}");
                        Console.WriteLine($"  Descrição Disciplina: {cursoCursoDisciplina.Descricao}");
                        Console.WriteLine("  ------------------------------------------");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }

        public static void ListaCursoPorId()
        {
            using (var context = new ExemploContext())
            {
                Console.Write("Informe o Id do Curso: ");
                var cursoId = Guid.Parse(Console.ReadLine());

                var curso = context.CursoSet.FirstOrDefault(p => p.Id == cursoId);

                Console.WriteLine();
                Console.WriteLine($"Id: {curso.Id}");
                Console.WriteLine($"Descrição: {curso.Descricao}");
            }
        }

        public static void ListaCursoOrdemCrescente()
        {
            using (var context = new ExemploContext())
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Listagem de Cursos");
                Console.WriteLine("------------------------------------------");
                foreach (var curso in context.CursoSet.OrderBy(p => p.Descricao).ToList())
                {
                    Console.WriteLine($"Id: {curso.Id}");
                    Console.WriteLine($"Descrição: {curso.Descricao}");
                    Console.WriteLine("------------------------------------------");
                }
            }
        }

        public static void ListaCursoOrdemDecrescente()
        {
            using (var context = new ExemploContext())
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Listagem de Cursos");
                Console.WriteLine("------------------------------------------");
                foreach (var curso in context.CursoSet.OrderByDescending(p => p.Descricao).ToList())
                {
                    Console.WriteLine($"Id: {curso.Id}");
                    Console.WriteLine($"Descrição: {curso.Descricao}");
                    Console.WriteLine("------------------------------------------");
                }
            }
        }
    }
}
