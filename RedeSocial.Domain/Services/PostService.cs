using RedeSocial.Domain.Entities;
using RedeSocial.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeSocial.Domain.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {

            _postRepository = postRepository;
        }
        public Post ConsultarPost(Guid id)
        {
            return _postRepository.ConsultarPost(id);
        }
        public void CriarPost(Post post)
        {
            _postRepository.CriarPost(post);
        }
        public bool AlterarPost(Post post)
        {

            var result = _postRepository.AlterarPost(post);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
        public ICollection<Post> ConsultarPosts()
        {
            return _postRepository.ConsultarPosts();
        }
        public ICollection<Comentario> ConsultarComentarios()
        {
            return _postRepository.ConsultarComentarios();
        }
        public void CriarComentario(Comentario comentario)
        {
           _postRepository.CriarComentario(comentario);
        }

        public IEnumerable<Post> GetAllPostsByPerfil(string perfil)
        {
            return _postRepository.GetAllPostsByPerfil(perfil);
        }

        public bool ExcluirPost(Guid id)
        {

            var result = _postRepository.ExcluirPost(id);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
    }
}
