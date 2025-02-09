// Region Theme
function setTheme(theme, element) {
    document.querySelectorAll('.btn-theme').forEach((btn) => {
        btn.classList.remove('active');
        btn.classList.add('inactive');
    });

    element.classList.remove('inactive');
    element.classList.add('active');

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

    buttons.forEach((btn) => {
        if (btn.getAttribute("data-theme") === savedTheme) {
            setTheme(savedTheme, btn);
        }
    });
};

// endregion Theme
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

// Função para sortear um livro
function randomizarLivro() {
    fetch("/biblia/sortearLivro")
        .then(response => {
            if (!response.ok) {
                throw new Error("Erro ao buscar livro");
            }
            return response.json();
        })
        .then(data => {
            console.log("Livro recebido:", data); // Log para depuração
            let livroSorteado = document.getElementById("livroSorteado");

            if (livroSorteado && data && data.nome) {
                livroSorteado.innerText = `📖 Livro: ${data.nome}`;
                livroSorteado.style.display = "block";
                livroSorteado.classList.add("show-resultado");
                buscarResumo(data.id);
            } else {
                console.error("Elemento livroSorteado não encontrado ou dados inválidos.");
            }
        })
        .catch(error => console.error("Erro ao sortear livro:", error));
}

// Função para buscar o resumo do livro sorteado
function buscarResumo(id) {
    fetch(`/biblia/buscarResumo/${id}`)
        .then(response => response.json())
        .then(data => {
            console.log("Resumo recebido:", data); // Log para depuração
            let resumoSorteado = document.getElementById("resumoSorteado");

            if (resumoSorteado && data.resumo) {
                resumoSorteado.innerText = `📜 Resumo do livro: ${data.resumo}`;
                resumoSorteado.style.display = "block";
                resumoSorteado.classList.add("show-resultado");
            } else {
                console.error("Elemento resumoSorteado não encontrado ou resumo inválido.");
            }
        })
        .catch(error => console.error("Erro ao buscar resumo:", error));
}

function randomizarLivroCapitulo() {
    fetch("/biblia/sortearLivroComCapitulo")
        .then(response => {
            if (!response.ok) {
                throw new Error("Erro ao buscar livro e capítulo");
            }
            return response.json();
        })
        .then(data => {
            document.getElementById("livroSorteado").innerText = `📖 Livro: ${data.nome} - 📜 Capítulo: ${data.capitulo}`;
            document.getElementById("livroSorteado").style.display = "block";

            buscarResumo(data.id);
        })
        .catch(error => console.error("Erro ao sortear livro e capítulo:", error));
}



// endregion Button Action
