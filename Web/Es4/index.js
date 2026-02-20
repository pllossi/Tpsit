const fs = require('fs');

document.addEventListener("DOMContentLoaded", () => {
    console.log("Pagina caricata");
    let emailInput = document.getElementById("email");
    let passwordInput = document.getElementById("password");
    //logica per prendere i dati del login per poi salvarli in un json
    document.getElementById("loginForm").addEventListener("submit", (event) => {
        event.preventDefault();
        const email = emailInput.value;
        const password = passwordInput.value;
        const loginData = {
            email: email,
            password: password
        };
        const jsonData = JSON.stringify(loginData);
        fs.writeFile("loginData.json", jsonData);
    });
});