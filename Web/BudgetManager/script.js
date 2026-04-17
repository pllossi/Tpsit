document.addEventListener('DOMContentLoaded', () => {
    const budgetForm = document.getElementById('budgetForm');
    const nomeAttivita = document.getElementById('nomeAttivita');
    const costoPrevisto = document.getElementById('costoPrevisto');
    const taskList = document.getElementById('taskList');
    const totaleSpesa = document.getElementById('totaleSpesa');
    const ordinaMenoCostosi = document.getElementById('ordinaMenoCostosi');
    const ordinaPiuCostosi = document.getElementById('ordinaPiuCostosi');
    
    listaSpese = JSON.parse(localStorage.getItem('listaSpese')) || [];

    aggiornaUI();

    budgetForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const nome = nomeAttivita.value.trim();
        const costo = parseFloat(costoPrevisto.value);

        if (nome === '' || isNaN(costo) || costo <= 0) {
            alert('Inserisci un nome valido e un costo maggiore di zero.');
            return;
        }

        listaSpese.push({ nome, costo });
        salvaDati();
        aggiornaUI();

        budgetForm.reset();
    });

    window.eliminaTask = function (index) {
        listaSpese.splice(index, 1);
        salvaDati();
        aggiornaUI();
    };

    ordinaMenoCostosi.addEventListener('click', () => {
        listaSpese.sort((a, b) => a.costo - b.costo);
        aggiornaUI();
    });

    ordinaPiuCostosi.addEventListener('click', () => {
        listaSpese.sort((a, b) => b.costo - a.costo);
        aggiornaUI();
    });

    function aggiornaUI() {
        taskList.innerHTML = '';
        let totale = 0;

        listaSpese.forEach((spesa, index) => {
            totale += spesa.costo;

            const li = document.createElement('li');
            li.className = 'list-group-item d-flex justify-content-between align-items-center text-dark';
            li.innerHTML = `
                <span><strong>${spesa.nome}</strong> - ${spesa.costo.toFixed(2)} €</span>
                <button class="btn btn-danger btn-sm" onclick="eliminaTask(${index})">Elimina</button>
            `;
            taskList.appendChild(li);
        });

        totaleSpesa.textContent = totale.toFixed(2);

        const h3Totale = totaleSpesa.parentElement;
        let avviso = document.getElementById('avvisoBudget');

        if (totale > 500) {
            h3Totale.classList.add('text-danger');
            if (!avviso) {
                avviso = document.createElement('span');
                avviso.id = 'avvisoBudget';
                avviso.className = 'fs-5 fw-bold ms-3';
                avviso.textContent = 'Budget Superato!';
                h3Totale.appendChild(avviso);
            }
        } else {
            h3Totale.classList.remove('text-danger');
            if (avviso) {
                avviso.remove();
            }
        }
    }

    function salvaDati() {
        localStorage.setItem('listaSpese', JSON.stringify(listaSpese));
    }
});


