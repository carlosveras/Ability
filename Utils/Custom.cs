using System;

namespace Ability.Web.Utils
{
    public class CustomException : Exception
    {

        public CustomException() { }

        public CustomException(string message)
            : base(message) { }

        public CustomException(string message, Exception innerException)
            : base(message, innerException) { }

        public string Mensagem { get; set; } = "Erro ao acessar o banco de dados, informe a àrea de T.I.";
    }
}

