const container = document.getElementById("container");
const statusText = document.getElementById("status");
const refreshButton = document.getElementById("refresh");

function setStatus(message, type = "info") {
  statusText.textContent = message;
  statusText.className = type === "error" ? "status-error" : "status-ok";
}

function renderCards(data) {
  container.innerHTML = "";

  data.forEach((item) => {
    const card = document.createElement("div");
    const isOnline = item.id % 2 === 0;

    card.classList.add("card", isOnline ? "online" : "offline");
    card.innerHTML = `
      <h3>Risorsa #${item.id}</h3>
      <p>${item.title}</p>
      <div class="status-label">${isOnline ? "ONLINE" : "OFFLINE"}</div>
    `;

    container.appendChild(card);
  });
}

async function fetchData() {
  refreshButton.disabled = true;
  setStatus("Aggiornamento in corso...");

  try {
    const response = await fetch("https://jsonplaceholder.typicode.com/posts?_limit=10");
    if (!response.ok) {
      throw new Error("Errore nella risposta del server");
    }

    const data = await response.json();
    renderCards(data);

    const onlineCount = data.filter((item) => item.id % 2 === 0).length;
    const offlineCount = data.length - onlineCount;
    setStatus(`Totale: ${data.length} risorse - Online: ${onlineCount}, Offline: ${offlineCount}`);
  } catch (error) {
    container.innerHTML = "";
    setStatus("Errore nel caricamento dei dati. Riprova.", "error");
    console.error(error);
  } finally {
    refreshButton.disabled = false;
  }
}

refreshButton.addEventListener("click", fetchData);
fetchData();
