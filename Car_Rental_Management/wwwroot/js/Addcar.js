let cars = [];

// Image Preview
document.getElementById("images").addEventListener("change", function () {
    const preview = document.getElementById("imagesPreview");
    preview.innerHTML = "";
    Array.from(this.files).forEach(file => {
        const reader = new FileReader();
        reader.onload = function (e) {
            const img = document.createElement("img");
            img.src = e.target.result;
            img.style.width = "60px";
            img.style.height = "60px";
            img.style.objectFit = "cover";
            img.style.marginRight = "5px";
            img.style.marginBottom = "5px";
            img.classList.add("rounded");
            preview.appendChild(img);
        };
        reader.readAsDataURL(file);
    });
});

// Add/Edit Car Form Submit
document.getElementById("addCarForm").addEventListener("submit", function (e) {
    e.preventDefault();

    let imageFiles = document.getElementById("images").files;
    let imagesBase64 = [];

    Array.from(imageFiles).forEach(file => {
        const reader = new FileReader();
        reader.onload = function (e) {
            imagesBase64.push(e.target.result);
            if (imagesBase64.length === imageFiles.length) {
                saveCar(imagesBase64);
            }
        };
        reader.readAsDataURL(file);
    });

    if (imageFiles.length === 0) {
        saveCar([]);
    }
});

function saveCar(imagesBase64) {
    let editId = document.getElementById("editCarId").value;

    if (editId) {
        let car = cars.find(c => c.id == editId);
        car.make = document.getElementById("make").value;
        car.model = document.getElementById("model").value;
        car.year = document.getElementById("year").value;
        car.plate = document.getElementById("plate").value;
        car.seats = document.getElementById("seats").value;
        car.transmission = document.getElementById("transmission").value;
        car.fuel = document.getElementById("fuel").value;
        car.mileage = document.getElementById("mileage").value;
        car.price = document.getElementById("price").value;
        car.color = document.getElementById("color").value;
        car.available = document.getElementById("isAvailable").checked;
        car.description = document.getElementById("description").value;
        if (imagesBase64.length > 0) car.images = imagesBase64;
    } else {
        let car = {
            id: Date.now(),
            make: document.getElementById("make").value,
            model: document.getElementById("model").value,
            year: document.getElementById("year").value,
            plate: document.getElementById("plate").value,
            seats: document.getElementById("seats").value,
            transmission: document.getElementById("transmission").value,
            fuel: document.getElementById("fuel").value,
            mileage: document.getElementById("mileage").value,
            price: document.getElementById("price").value,
            color: document.getElementById("color").value,
            available: document.getElementById("isAvailable").checked,
            description: document.getElementById("description").value,
            images: imagesBase64
        };
        cars.push(car);
    }

    renderCars();
    document.getElementById("addCarForm").reset();
    document.getElementById("imagesPreview").innerHTML = "";
    document.getElementById("editCarId").value = "";
    document.getElementById("carModalTitle").innerText = "Add New Car";
    let modal = bootstrap.Modal.getInstance(document.getElementById("addCarModal"));
    modal.hide();
}

// Render Cars
function renderCars() {
    let container = document.getElementById("carsContainer");
    let emptyState = document.getElementById("emptyState");
    container.innerHTML = "";

    if (cars.length === 0) {
        emptyState.style.display = "block";
        return;
    }
    emptyState.style.display = "none";

    cars.forEach(car => {
        let card = document.createElement("div");
        card.className = "col-md-4";
        card.innerHTML = `
      <div class="car-card">
        <div>
          <strong>${car.make} ${car.model}</strong><br>
          <small>${car.year} • ${car.plate}</small><br>
          <span class="badge ${car.available ? 'badge-available' : 'badge-unavailable'}">
            ${car.available ? 'Available' : 'Unavailable'}
          </span>
        </div>
        <div>
          <button class="btn btn-sm btn-info me-2" onclick="viewCar(${car.id})">View</button>
          <button class="btn btn-sm btn-warning me-2" onclick="editCar(${car.id})">Edit</button>
          <button class="btn btn-sm btn-danger" onclick="deleteCar(${car.id})">Delete</button>
        </div>
      </div>`;
        container.appendChild(card);
    });
}

// View, Edit, Delete Functions
function viewCar(id) {
    let car = cars.find(c => c.id === id);
    let imagesHtml = "";
    if (car.images.length > 0) {
        imagesHtml = '<div class="d-flex flex-wrap mb-3">';
        car.images.forEach(img => {
            imagesHtml += `<img src="${img}" style="width:80px;height:80px;object-fit:cover;margin-right:5px;margin-bottom:5px;" class="rounded">`;
        });
        imagesHtml += '</div>';
    }

    let details = `
    ${imagesHtml}
    <p><strong>Make:</strong> ${car.make}</p>
    <p><strong>Model:</strong> ${car.model}</p>
    <p><strong>Year:</strong> ${car.year}</p>
    <p><strong>Plate:</strong> ${car.plate}</p>
    <p><strong>Seats:</strong> ${car.seats}</p>
    <p><strong>Transmission:</strong> ${car.transmission}</p>
    <p><strong>Fuel:</strong> ${car.fuel}</p>
    <p><strong>Mileage:</strong> ${car.mileage}</p>
    <p><strong>Price/day:</strong> ${car.price}</p>
    <p><strong>Color:</strong> ${car.color}</p>
    <p><strong>Status:</strong> ${car.available ? "Available" : "Unavailable"}</p>
    <p><strong>Description:</strong> ${car.description}</p>
  `;
    document.getElementById("carDetailsBody").innerHTML = details;
    new bootstrap.Modal(document.getElementById("viewCarModal")).show();
}

function editCar(id) {
    let car = cars.find(c => c.id === id);
    if (!car) return;

    document.getElementById("carModalTitle").innerText = "Edit Car";
    document.getElementById("editCarId").value = car.id;
    document.getElementById("make").value = car.make;
    document.getElementById("model").value = car.model;
    document.getElementById("year").value = car.year;
    document.getElementById("plate").value = car.plate;
    document.getElementById("seats").value = car.seats;
    document.getElementById("transmission").value = car.transmission;
    document.getElementById("fuel").value = car.fuel;
    document.getElementById("mileage").value = car.mileage;
    document.getElementById("price").value = car.price;
    document.getElementById("color").value = car.color;
    document.getElementById("isAvailable").checked = car.available;
    document.getElementById("description").value = car.description;

    new bootstrap.Modal(document.getElementById("addCarModal")).show();
}

function deleteCar(id) {
    cars = cars.filter(c => c.id !== id);
    renderCars();
}
