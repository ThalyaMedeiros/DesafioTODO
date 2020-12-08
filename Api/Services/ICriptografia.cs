namespace Api.Services
{
    public interface ICriptografia
    {
        string Codifica(string senha);
        bool Compara(string senha, string hash);
    }
}