
function setTheme(theme, element = null) {
    document.querySelectorAll('.btn-theme').forEach((btn) => {
        btn.classList.remove('active');
        btn.classList.add('inactive');
    });

    if (element) {
        element.classList.remove('inactive');
        element.classList.add('active');
    }

    document.documentElement.classList.remove('theme-blue', 'theme-pink');
    if (theme === 'blue') {
        document.documentElement.classList.add('theme-blue');
    } else if (theme === 'pink') {
        document.documentElement.classList.add('theme-pink');
    }

    localStorage.setItem('theme', theme);
}

window.onload = function () {
    const savedTheme = localStorage.getItem('theme') || 'default';
    const buttons = document.querySelectorAll('.btn-theme');

    let found = false;
    buttons.forEach((btn) => {
        if (btn.getAttribute("data-theme") === savedTheme) {
            setTheme(savedTheme, btn);
            found = true;
        }
    });

    if (!found) {
        const defaultBtn = document.querySelector('.btn-theme[data-theme="default"]');
        if (defaultBtn) {
            setTheme('default', defaultBtn);
        }
    }
};

// region Button Action

document.addEventListener("DOMContentLoaded", function () {
    let livroSorteado = document.getElementById("livroSorteado");
    let resumoSorteado = document.getElementById("resumoSorteado");

    // Verifica se os elementos existem antes de tentar acessá-los
    if (livroSorteado) {
        livroSorteado.style.display = "none";
    }
    if (resumoSorteado) {
        resumoSorteado.style.display = "none";
    }

    let modal = document.getElementById("devocionalModal");
    if (modal) {
        modal.addEventListener("hidden.bs.modal", function () {
            if (livroSorteado) {
                livroSorteado.innerHTML = "";
                livroSorteado.style.display = "none";
            }
            if (resumoSorteado) {
                resumoSorteado.innerHTML = "";
                resumoSorteado.style.display = "none";
            }
        });
    }
});
function randomizarLivro() {
    fetch("/biblia/sortearLivro")
        .then(response => response.json())
        .then(data => {
            console.log("Livro recebido:", data); 
            let livroSorteado = document.getElementById("livroSorteado");
            let resumoSorteado = document.getElementById("resumoSorteado");

            if (livroSorteado && resumoSorteado) {
                livroSorteado.innerText = `📖 Livro: ${data.nome}`;
                livroSorteado.style.display = "block";
                livroSorteado.classList.add("show-resultado");
                buscarResumo(data.id);
            }
        })
        .catch(error => console.error("Erro ao sortear livro:", error));
}

function buscarResumo(id) {
    fetch(`/biblia/buscarResumo/${id}`)
        .then(response => response.json())
        .then(data => {
            console.log("Resumo recebido:", data);
            let resumoSorteado = document.getElementById("resumoSorteado");

            if (resumoSorteado) {
                resumoSorteado.innerText = `📜 Resumo: ${data.resumo}`;
                resumoSorteado.style.display = "block";
                resumoSorteado.classList.add("show-resultado");
            }
        })
        .catch(error => console.error("Erro ao buscar resumo:", error));
}

function randomizarLivroCapitulo() {
    fetch("/biblia/sortearLivroComCapitulo")
        .then(response => response.json())
        .then(data => {
            console.log("Livro e capítulo recebidos:", data);
            let livroSorteado = document.getElementById("livroSorteado");
            let resumoSorteado = document.getElementById("resumoSorteado");

            if (livroSorteado && resumoSorteado) {
                livroSorteado.innerText = `📖 Livro: ${data.nome} - 📜 Capítulo: ${data.capitulo}`;
                livroSorteado.style.display = "block";
                livroSorteado.classList.add("show-resultado");
                buscarResumo(data.id);
            }
        })
        .catch(error => console.error("Erro ao sortear livro e capítulo:", error));
}



// endregion Button Action
