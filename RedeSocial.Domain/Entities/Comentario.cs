namespace RedeSocial.Domain.Entities {
    public class Comentario {

        public Guid Id { get; set; }
        public string Texto { get; set; }
        public DateTime Data { get; set; }
        public Perfil Perfil { get; set; }
        public Post Post { get; set; }
    }
}
