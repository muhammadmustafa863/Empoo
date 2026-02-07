const currency = new Intl.NumberFormat("en-US", {
  style: "currency",
  currency: "USD",
});

const state = {
  cart: [],
  heldSale: null,
  products: [
    {
      id: "PRD-001",
      name: "Organic Bananas",
      category: "Produce",
      cost: 0.72,
      price: 1.29,
      stock: 92,
    },
    {
      id: "PRD-002",
      name: "Whole Milk 1L",
      category: "Dairy",
      cost: 2.1,
      price: 3.49,
      stock: 38,
    },
    {
      id: "PRD-003",
      name: "Brown Eggs (12ct)",
      category: "Dairy",
      cost: 2.6,
      price: 4.25,
      stock: 24,
    },
    {
      id: "PRD-004",
      name: "Avocado Pack",
      category: "Produce",
      cost: 3.55,
      price: 5.99,
      stock: 12,
    },
    {
      id: "PRD-005",
      name: "Sparkling Water",
      category: "Beverages",
      cost: 0.84,
      price: 1.75,
      stock: 54,
    },
    {
      id: "PRD-006",
      name: "Bakery Sourdough",
      category: "Bakery",
      cost: 2.95,
      price: 4.85,
      stock: 16,
    },
  ],
  customers: [
    {
      id: "CUST-001",
      name: "Arianna Smith",
      phone: "(555) 019-3321",
      points: 140,
      memberSince: "2022-05-16",
    },
    {
      id: "CUST-002",
      name: "Jun Park",
      phone: "(555) 010-2048",
      points: 90,
      memberSince: "2023-02-11",
    },
  ],
  reports: [
    {
      time: "09:12",
      receipt: "R-1821",
      cashier: "Maya",
      items: 6,
      total: 42.18,
      status: "Completed",
    },
    {
      time: "10:07",
      receipt: "R-1822",
      cashier: "Jordan",
      items: 3,
      total: 18.42,
      status: "Completed",
    },
    {
      time: "11:22",
      receipt: "R-1823",
      cashier: "Maya",
      items: 4,
      total: 21.05,
      status: "Refunded",
    },
  ],
};

const productGrid = document.getElementById("product-grid");
const cartItems = document.getElementById("cart-items");
const subtotalEl = document.getElementById("subtotal");
const taxEl = document.getElementById("tax");
const totalEl = document.getElementById("total");
const discountEl = document.getElementById("discount");
const productTable = document.getElementById("product-table");
const customerTable = document.getElementById("customer-table");
const reportTable = document.getElementById("report-table");
const lowStockList = document.getElementById("low-stock");
const modal = document.getElementById("modal");
const modalForm = document.getElementById("modal-form");
const modalTitle = document.getElementById("modal-title");

const navButtons = document.querySelectorAll(".nav-item");
const panels = document.querySelectorAll(".panel");

const modalState = {
  type: null,
  itemId: null,
};

const openModal = (type, itemId = null) => {
  modalState.type = type;
  modalState.itemId = itemId;
  modalForm.innerHTML = "";

  if (type === "product") {
    modalTitle.textContent = itemId ? "Edit Product" : "Add Product";
    buildModalInput("SKU", "id", itemId);
    buildModalInput("Name", "name", itemId);
    buildModalInput("Category", "category", itemId);
    buildModalInput("Cost Price", "cost", itemId, "number");
    buildModalInput("Stock", "stock", itemId, "number");
    buildModalInput("Price", "price", itemId, "number");
  }

  if (type === "customer") {
    modalTitle.textContent = itemId ? "Edit Customer" : "Add Customer";
    buildModalInput("Name", "name", itemId);
    buildModalInput("Phone", "phone", itemId);
    buildModalInput("Points", "points", itemId, "number");
    buildModalInput("Member Since", "memberSince", itemId, "date");
  }

  modal.classList.add("active");
};

const buildModalInput = (label, field, itemId, type = "text") => {
  const wrapper = document.createElement("label");
  wrapper.textContent = label;
  const input = document.createElement("input");
  input.type = type;
  input.name = field;

  if (itemId) {
    const item =
      modalState.type === "product"
        ? state.products.find((entry) => entry.id === itemId)
        : state.customers.find((entry) => entry.id === itemId);
    if (item && item[field] !== undefined) {
      input.value = item[field];
    }
  }

  wrapper.appendChild(input);
  modalForm.appendChild(wrapper);
};

const closeModal = () => {
  modal.classList.remove("active");
};

const updateCart = () => {
  cartItems.innerHTML = "";
  state.cart.forEach((item) => {
    const row = document.createElement("div");
    row.className = "cart-item";

    const name = document.createElement("div");
    name.innerHTML = `<strong>${item.name}</strong><br /><span class="muted">${item.id}</span>`;

    const qty = document.createElement("input");
    qty.type = "number";
    qty.min = "1";
    qty.value = item.quantity;
    qty.addEventListener("change", (event) => {
      item.quantity = Math.max(1, Number(event.target.value));
      updateCart();
    });

    const price = document.createElement("div");
    price.innerHTML = `${currency.format(item.price * item.quantity)}<br /><button data-id="${item.id}" class="link">Remove</button>`;

    row.append(name, qty, price);
    cartItems.appendChild(row);
  });

  const subtotal = state.cart.reduce((sum, item) => sum + item.price * item.quantity, 0);
  const discount = Number(discountEl.value) || 0;
  const taxRate = Number(document.getElementById("tax-rate").value || 7.5) / 100;
  const tax = subtotal * taxRate;
  const total = Math.max(0, subtotal + tax - discount);

  subtotalEl.textContent = currency.format(subtotal);
  taxEl.textContent = currency.format(tax);
  totalEl.textContent = currency.format(total);

  document.querySelectorAll(".link").forEach((btn) => {
    btn.addEventListener("click", () => {
      state.cart = state.cart.filter((item) => item.id !== btn.dataset.id);
      updateCart();
    });
  });
};

const renderProducts = (filter = "") => {
  productGrid.innerHTML = "";
  const normalizedFilter = filter.toLowerCase();
  const filtered = state.products.filter(
    (product) =>
      product.name.toLowerCase().includes(normalizedFilter) ||
      product.id.toLowerCase().includes(normalizedFilter)
  );

  filtered.forEach((product) => {
    const card = document.createElement("div");
    card.className = "product-card";
    card.innerHTML = `
      <strong>${product.name}</strong>
      <span class="muted">${product.category}</span>
      <span>Stock: ${product.stock}</span>
      <span>${currency.format(product.price)}</span>
    `;

    const button = document.createElement("button");
    button.textContent = "Add";
    button.addEventListener("click", () => addToCart(product));

    card.appendChild(button);
    productGrid.appendChild(card);
  });
};

const renderProductTable = (filter = "") => {
  productTable.innerHTML = "";
  const normalizedFilter = filter.toLowerCase();
  const filtered = state.products.filter(
    (product) =>
      product.name.toLowerCase().includes(normalizedFilter) ||
      product.id.toLowerCase().includes(normalizedFilter)
  );
  filtered.forEach((product) => {
    const row = document.createElement("div");
    row.className = "table-row";
    row.innerHTML = `
      <span>${product.id}</span>
      <span>${product.name}</span>
      <span>${product.category}</span>
      <span>${currency.format(product.cost ?? 0)}</span>
      <span>${product.stock}</span>
      <span>${currency.format(product.price)}</span>
      <span>
        <button class="link" data-action="edit" data-id="${product.id}">Edit</button>
        <button class="link" data-action="delete" data-id="${product.id}">Delete</button>
      </span>
    `;
    productTable.appendChild(row);
  });
};

const renderProfitInsights = () => {
  const profitPotential = state.products.reduce(
    (sum, product) => sum + (product.price - (product.cost ?? 0)) * product.stock,
    0
  );
  const revenue = state.products.reduce(
    (sum, product) => sum + product.price * product.stock,
    0
  );
  const margin = revenue > 0 ? (profitPotential / revenue) * 100 : 0;

  document.getElementById("profit-potential").textContent =
    currency.format(profitPotential);
  document.getElementById("profit-margin").textContent = `${margin.toFixed(1)}%`;
};

const renderCustomerTable = () => {
  customerTable.innerHTML = "";
  state.customers.forEach((customer) => {
    const row = document.createElement("div");
    row.className = "table-row";
    row.innerHTML = `
      <span>${customer.name}</span>
      <span>${customer.phone}</span>
      <span>${customer.points}</span>
      <span>${customer.memberSince}</span>
      <span>
        <button class="link" data-action="edit" data-id="${customer.id}">Edit</button>
        <button class="link" data-action="delete" data-id="${customer.id}">Delete</button>
      </span>
    `;
    customerTable.appendChild(row);
  });
};

const renderReports = () => {
  reportTable.innerHTML = "";
  state.reports.forEach((entry) => {
    const row = document.createElement("div");
    row.className = "table-row";
    row.innerHTML = `
      <span>${entry.time}</span>
      <span>${entry.receipt}</span>
      <span>${entry.cashier}</span>
      <span>${entry.items}</span>
      <span>${currency.format(entry.total)}</span>
      <span>${entry.status}</span>
    `;
    reportTable.appendChild(row);
  });

  const lowStock = state.products.filter((product) => product.stock < 20);
  lowStockList.innerHTML = "";
  lowStock.forEach((product) => {
    const item = document.createElement("li");
    item.textContent = `${product.name} (${product.stock})`;
    lowStockList.appendChild(item);
  });
};

const addToCart = (product) => {
  const existing = state.cart.find((item) => item.id === product.id);
  if (existing) {
    existing.quantity += 1;
  } else {
    state.cart.push({ ...product, quantity: 1 });
  }
  updateCart();
};

const handleModalSave = () => {
  const formData = new FormData(modalForm);
  const values = Object.fromEntries(formData.entries());

  if (modalState.type === "product") {
    const payload = {
      id: values.id || `PRD-${Date.now()}`,
      name: values.name,
      category: values.category,
      cost: Number(values.cost || 0),
      stock: Number(values.stock || 0),
      price: Number(values.price || 0),
    };

    if (modalState.itemId) {
      state.products = state.products.map((product) =>
        product.id === modalState.itemId ? payload : product
      );
    } else {
      state.products.push(payload);
    }
    renderProducts();
    renderProductTable();
    renderProfitInsights();
  }

  if (modalState.type === "customer") {
    const payload = {
      id: modalState.itemId || `CUST-${Date.now()}`,
      name: values.name,
      phone: values.phone,
      points: Number(values.points || 0),
      memberSince: values.memberSince,
    };

    if (modalState.itemId) {
      state.customers = state.customers.map((customer) =>
        customer.id === modalState.itemId ? payload : customer
      );
    } else {
      state.customers.push(payload);
    }
    renderCustomerTable();
  }

  closeModal();
};

const setupEventListeners = () => {
  navButtons.forEach((button) => {
    button.addEventListener("click", () => {
      navButtons.forEach((btn) => btn.classList.remove("active"));
      panels.forEach((panel) => panel.classList.remove("active"));
      button.classList.add("active");
      document.getElementById(button.dataset.panel).classList.add("active");
    });
  });

  document.getElementById("product-search").addEventListener("input", (event) => {
    renderProducts(event.target.value);
  });

  document.getElementById("product-filter").addEventListener("input", (event) => {
    renderProductTable(event.target.value);
  });

  document.getElementById("barcode-input").addEventListener("change", (event) => {
    const match = state.products.find(
      (product) => product.id.toLowerCase() === event.target.value.toLowerCase()
    );
    if (match) {
      addToCart(match);
    }
    event.target.value = "";
  });

  document.getElementById("clear-cart").addEventListener("click", () => {
    state.cart = [];
    updateCart();
  });

  document.getElementById("hold-sale").addEventListener("click", () => {
    state.heldSale = [...state.cart];
    state.cart = [];
    updateCart();
  });

  document.getElementById("resume-sale").addEventListener("click", () => {
    if (state.heldSale) {
      state.cart = [...state.heldSale];
      state.heldSale = null;
      updateCart();
    }
  });

  document.querySelectorAll(".pay").forEach((button) => {
    button.addEventListener("click", () => {
      alert(`Payment method selected: ${button.dataset.method}`);
    });
  });

  document.getElementById("complete-sale").addEventListener("click", () => {
    if (!state.cart.length) {
      alert("Add items to the cart before completing the sale.");
      return;
    }
    state.cart = [];
    updateCart();
    alert("Sale completed. Receipt sent to printer.");
  });

  discountEl.addEventListener("input", updateCart);

  document.getElementById("add-product").addEventListener("click", () => {
    openModal("product");
  });

  document.getElementById("add-customer").addEventListener("click", () => {
    openModal("customer");
  });

  document.getElementById("save-modal").addEventListener("click", (event) => {
    event.preventDefault();
    handleModalSave();
  });

  document.getElementById("cancel-modal").addEventListener("click", (event) => {
    event.preventDefault();
    closeModal();
  });

  document.getElementById("product-table").addEventListener("click", (event) => {
    if (!event.target.dataset.action) return;
    const id = event.target.dataset.id;
    if (event.target.dataset.action === "edit") {
      openModal("product", id);
    } else {
      state.products = state.products.filter((product) => product.id !== id);
      renderProducts();
      renderProductTable();
      renderProfitInsights();
    }
  });

  document.getElementById("customer-table").addEventListener("click", (event) => {
    if (!event.target.dataset.action) return;
    const id = event.target.dataset.id;
    if (event.target.dataset.action === "edit") {
      openModal("customer", id);
    } else {
      state.customers = state.customers.filter((customer) => customer.id !== id);
      renderCustomerTable();
    }
  });

  document.getElementById("close-shift").addEventListener("click", () => {
    document.getElementById("shift-status").textContent = "Closed";
    document.getElementById("cash-drawer").textContent = "$0.00";
  });

  document.getElementById("save-store").addEventListener("click", () => {
    alert("Store settings saved.");
  });

  document.getElementById("test-print").addEventListener("click", () => {
    alert("Printer test sent.");
  });

  document.getElementById("manage-users").addEventListener("click", () => {
    alert("User management panel opened.");
  });

  document.getElementById("export-report").addEventListener("click", () => {
    alert("CSV export generated.");
  });
};

renderProducts();
renderProductTable();
renderCustomerTable();
renderReports();
renderProfitInsights();
updateCart();
setupEventListeners();
