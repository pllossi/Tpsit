const voti = [];
const votoInput = document.getElementById('votoInput');
const aggiungiBtn = document.getElementById('aggiungiBtn');
const votiList = document.getElementById('votiList');
const errorMsg = document.getElementById('errorMsg');
const risultato = document.getElementById('risultato');
const mediaValore = document.getElementById('mediaValore');
const icona = document.getElementById('icona');
const noVotiMsg = document.getElementById('noVotiMsg');

aggiungiBtn.addEventListener('click', () => {
    const stringValue = votoInput.value.replace(',', '.');
    const valore = parseFloat(stringValue);

    if (isNaN(valore) || valore < 1 || valore > 10) {
        errorMsg.classList.remove('d-none');
        return;
    }

    errorMsg.classList.add('d-none');
    voti.push(valore);
    votoInput.value = '';

    aggiornaLista();
    calcolaMedia();
});

function aggiornaLista() {
    if (voti.length > 0 && noVotiMsg) {
        noVotiMsg.style.display = 'none';
    }
    
    votiList.innerHTML = '';
    
    voti.forEach(voto => {
        const badge = document.createElement('div');
        badge.className = 'voto-badge ' + (voto >= 6 ? 'voto-positivo' : voto >= 5 ? 'voto-quasi-positivo' : 'voto-negativo');

        let displayVoto = voto;
        if(Number.isInteger(voto)) {
            displayVoto = voto.toString(); 
        } else {
            displayVoto = voto.toString().replace('.', ','); 
        }
        
        badge.textContent = displayVoto;
        votiList.appendChild(badge);
    });
}

function calcolaMedia() {
    if (voti.length === 0) return;

    const somma = voti.reduce((acc, curr) => acc + curr, 0);
    const media = somma / voti.length;

    risultato.classList.remove('d-none');
    mediaValore.textContent = media.toFixed(2);

    if (media < 6) {
        mediaValore.className = 'text-danger';
        icona.textContent = 'Studia di più!';
    } else {
        mediaValore.className = 'text-success';
        icona.textContent = 'Ottimo lavoro!';
    }
}
