document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector("#converterForm");
    const resultBox = document.querySelector("#result");
    const conversionOutput = document.querySelector("#conversionOutput");

    form.addEventListener("submit", (event) => {
        event.preventDefault();

        // Validazione base: controllo che ci sia un valore valido
        const rawValue = document.querySelector("#inputValue").value;
        if (rawValue === "" || isNaN(rawValue)) {
            conversionOutput.textContent = "Inserisci un valore numerico valido.";
            resultBox.classList.add("show");
            return;
        }

        const inputValue = parseFloat(rawValue);
        const inputUnit = document.querySelector("#inputUnit").value;
        const outputUnit = document.querySelector("#outputUnit").value;

        if (inputUnit === outputUnit) {
            conversionOutput.textContent = `Risultato: ${inputValue} ${outputUnit} (nessuna conversione)`;
            resultBox.classList.add("show");
            return;
        }

        const toPx = {
            px: 1,
            rem: 16,
            cm: 96 / 2.54, 
            mm: (96 / 2.54) / 10
        };

        const valueInPx = inputValue * toPx[inputUnit];
        const outputValue = valueInPx / toPx[outputUnit];

        conversionOutput.textContent = `Risultato: ${parseFloat(outputValue.toFixed(4))} ${outputUnit}`;
        resultBox.classList.add("show");
    });
});