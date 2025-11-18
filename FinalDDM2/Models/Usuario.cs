using System.Collections.ObjectModel;

namespace FinalDDM2.Models;

public class Usuario
{
    public int Id { get; set; } = 0;
    public ObservableCollection<Clima> Buscas { get; set; } = [];
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; } = new DateTime(1930, 1, 1);
}