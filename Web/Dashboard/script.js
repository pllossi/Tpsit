
const API_URL = "https://jsonplaceholder.typicode.com/posts?_limit=10";

const dashboard   = document.getElementById("dashboard");
const loading     = document.getElementById("loading");
const errorState  = document.getElementById("error-state");
const errorMsg    = document.getElementById("error-msg");
const statTotal   = document.getElementById("stat-total");
const statOnline  = document.getElementById("stat-online");
const statOffline = document.getElementById("stat-offline");
const lastUpdate  = document.getElementById("last-update");
const clockEl     = document.getElementById("clock");

function updateClock() {
  const now = new Date();
  const hh  = String(now.getHours()).padStart(2, "0");
  const mm  = String(now.getMinutes()).padStart(2, "0");
  const ss  = String(now.getSeconds()).padStart(2, "0");
  clockEl.textContent = `${hh}:${mm}:${ss}`;
}
setInterval(updateClock, 1000);
updateClock();

function showLoading() {
  loading.classList.remove("hidden");
  dashboard.classList.add("hidden");
  errorState.classList.add("hidden");
}

function showDashboard() {
  loading.classList.add("hidden");
  dashboard.classList.remove("hidden");
  errorState.classList.add("hidden");
}

function showError(message) {
  loading.classList.add("hidden");
  dashboard.classList.add("hidden");
  errorState.classList.remove("hidden");
  errorMsg.textContent = message;
}

function createCard(resource) {
  const isOnline = resource.id % 2 === 0;
  const statusClass = isOnline ? "online"  : "offline";
  const statusLabel = isOnline ? "ONLINE"  : "OFFLINE";

  const card = document.createElement("div");
  card.classList.add("card", statusClass);

  card.innerHTML = `
    <div class="card-header">
      <span class="card-id">NODE &gt; <span>#${String(resource.id).padStart(3, "0")}</span></span>
      <span class="status-badge">${statusLabel}</span>
    </div>
    <div class="card-title">${escapeHtml(resource.title)}</div>
    <div class="card-body">${escapeHtml(resource.body)}</div>
    <div class="card-footer">
      <span class="user-id">${resource.userId}</span>
      <span>POST_ID: ${resource.id}</span>
    </div>
  `;

  return card;
}

function escapeHtml(str) {
  const div = document.createElement("div");
  div.appendChild(document.createTextNode(str));
  return div.innerHTML;
}

function updateStats(resources) {
  const total   = resources.length;
  const online  = resources.filter(r => r.id % 2 === 0).length;
  const offline = total - online;

  statTotal.textContent   = total;
  statOnline.textContent  = online;
  statOffline.textContent = offline;
}

async function loadResources() {
  showLoading();

  await new Promise(resolve => setTimeout(resolve, 600));

  try {
    const response = await fetch(API_URL);

    if (!response.ok) {
      throw new Error(`HTTP ${response.status}: ${response.statusText}`);
    }

    const resources = await response.json();

    // Svuota la dashboard prima di ripopolarla
    dashboard.innerHTML = "";

    // Crea e inserisce ogni card
    resources.forEach(resource => {
      const card = createCard(resource);
      dashboard.appendChild(card);
    });

    // Aggiorna le statistiche
    updateStats(resources);

    // Timestamp ultimo aggiornamento
    const now = new Date();
    lastUpdate.textContent = `Ultimo aggiornamento: ${now.toLocaleTimeString("it-IT")}`;

    showDashboard();

  } catch (error) {
    console.error("Errore durante il recupero dei dati:", error);
    showError(`Impossibile connettersi all'endpoint. Dettaglio: ${error.message}`);
  }
}

document.addEventListener("DOMContentLoaded", loadResources);