function setTheme(theme, element) {
    // Remove classes de todos os botões
    document.querySelectorAll('.btn-theme').forEach((btn) => {
        btn.classList.remove('active');
        btn.classList.add('inactive');
    });

    // Adiciona a classe "active" ao botão selecionado
    element.classList.remove('inactive');
    element.classList.add('active');

    // Altera o tema no <html>
    document.documentElement.classList.remove('theme-blue', 'theme-pink');
    if (theme === 'blue') {
        document.documentElement.classList.add('theme-blue');
    } else if (theme === 'pink') {
        document.documentElement.classList.add('theme-pink');
    }
    localStorage.setItem('theme', theme); // Salva o tema escolhido
}

// Recupera o tema salvo ao carregar a página
window.onload = function () {
    const savedTheme = localStorage.getItem('theme') || 'default';
    const buttons = document.querySelectorAll('.btn-theme');
    buttons.forEach((btn) => {
        if (btn.onclick.toString().includes(`setTheme('${savedTheme}'`)) {
            btn.click();
        }
    });
};


