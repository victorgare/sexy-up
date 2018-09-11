namespace SexyUp.ApplicationCore.Constants
{
    public static class ProductStatus
    {
        /// <summary>
        /// Produto Ativo
        /// </summary>
        public const int Ativo = 1;

        /// <summary>
        /// Produto foi inativado pelo usuário, pode voltar a ser ativo
        /// </summary>
        public const int Inativo = 2;

        /// <summary>
        /// Quando o produto for bloqueado, ele não pode voltar ao status Ativo, usaremos este para bloquear produtos que serão armazenados para historico
        /// </summary>
        public const int Bloqueado = 3;
    }
}