using Microsoft.AspNetCore.Mvc;
using RpgApi.models;
using RpgApi.models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            //Colar os objetos da lista do chat aqui
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };

        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            List<Personagem> ListaNome = personagens.FindAll(p => p.Nome == nome);
            if (ListaNome.Count != 0)
            {
                return Ok(ListaNome);

            }
            else
            {
                return BadRequest("Nome não existe");
            }




        }

        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao([FromBody] Personagem personagem)
        {

            if (personagem.Inteligencia >= 30 && personagem.Defesa >= 10)
            {

                personagens.Add(personagem);
                return Ok(personagem);
            }
            else
            {
                return BadRequest("Personagem não atende aos requisitos mínimos de Defesa e Inteligência");
            }
        }

        [HttpPost("PostValidacaoMago")]
        public IActionResult PostValidacaoMago(Personagem Classe, int inteligencia, Personagem personagem)
        {
            if (personagem.Classe == ClasseEnum.Mago && inteligencia > 35)
            {
                personagens.Add(personagem);
                return Ok(personagem);

            }
            else
            {
                return BadRequest("Personagem Mago deve ter no minimo 35 de Inteligencia ou mais para ser incluso");
            }


        }

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago(Personagem Classe, int PontosVida)
        {

            Personagem clerigosemagos = personagens.Find(p => p.Classe == ClasseEnum.Cavaleiro);
            personagens.Remove(clerigosemagos);
            List<Personagem> ListaFinal = personagens.OrderByDescending(p => p.PontosVida).ToList();
            return Ok(ListaFinal);

        }

        [HttpGet("GetEstatistica")]
        public IActionResult GetEstatistica(Personagem Inteligencia)
        {
            int qtPersonagens = personagens.Count;
            int somInteligencia = personagens.Sum(p => p.Inteligencia);
            var informacoesGerais = new
            {
                QuantidadeDePersonagens = qtPersonagens,
                SomaDeInteligencias = somInteligencia
            };
            return Ok(informacoesGerais);

        }

        [HttpGet("GetByClasse")]
        public IActionResult GetByClasse()
        {
            

        }
    }

}