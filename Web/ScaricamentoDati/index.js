document.addEventListener("DOMContentLoaded", () => {
	const dataDirectory = "./data/";
	const forms = document.querySelectorAll("main form");

	forms.forEach((form) => {
		form.addEventListener("submit", async (event) => {
			event.preventDefault();

			const action = form.getAttribute("action");

			if (!action || !action.trim()) {
				alert("Percorso file non valido.");
				return;
			}

			const normalizedPath = action.trim();

			if (!normalizedPath.startsWith(dataDirectory)) {
				alert("Il file deve trovarsi dentro ./data/");
				return;
			}

			const fileName = normalizedPath.split("/").pop();

			if (!fileName) {
				alert("Nome file non valido.");
				return;
			}

			await startDownload(normalizedPath, fileName);
		});
	});
});

async function startDownload(path, fileName) {
	try {
		const response = await fetch(path);

		if (!response.ok) {
			throw new Error("File non trovato");
		}

		const fileBlob = await response.blob();
		const objectUrl = URL.createObjectURL(fileBlob);
		const link = document.createElement("a");

		link.href = objectUrl;
		link.download = fileName;
		link.style.display = "none";
		document.body.appendChild(link);
		link.click();
		document.body.removeChild(link);
		URL.revokeObjectURL(objectUrl);
	} catch (error) {
		alert("Impossibile scaricare il file richiesto.");
	}
}
