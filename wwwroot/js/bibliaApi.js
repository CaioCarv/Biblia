const apiKey = "241828a2e9f4def2387d9b2c82519e11"; // Substitua pela sua chave
const bibleId = "90799bb5b996fddc-01"; // ID da tradução brasileira

// Função para buscar um capítulo específico
async function fetchChapter(bookId, chapter) {
    const url = `https://api.scripture.api.bible/v1/bibles/${bibleId}/chapters/${bookId}.${chapter}?fums-version=3`;

    try {
        const response = await fetch(url, {
            method: "GET",
            headers: {
                "api-key": apiKey,
            },
        });

        if (!response.ok) {
            throw new Error(`Erro: ${response.status} - ${response.statusText}`);
        }

        const data = await response.json();
        console.log(`Conteúdo do livro ${bookId}, capítulo ${chapter}:`, data);
    } catch (error) {
        console.error("Erro ao buscar o capítulo:", error);
    }
}

// Função para chamar ao selecionar o livro e capítulo
function buscarLivroCapitulo() {
    const id = document.getElementById("id").value; // Exemplo: "GEN"
    const capitulo = document.getElementById("capitulo").value; // Exemplo: "1"
    fetchChapter(bookId, chapter);
}

// Exemplo de evento de botão para buscar o capítulo
document.getElementById("buscar").addEventListener("click", buscarLivroCapitulo);
