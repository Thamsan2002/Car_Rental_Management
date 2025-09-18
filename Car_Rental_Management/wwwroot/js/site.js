document.addEventListener("DOMContentLoaded", function () {
    // AOS Init
    AOS.init({
        duration: 1000,
        once: false
    });

    // Mobile Menu Toggle
    const menuBtn = document.getElementById("menuBtn");
    const navLinks = document.getElementById("navLinks");
    menuBtn.addEventListener("click", () => {
        navLinks.classList.toggle("active");
    });

    // Hero Slider
    const slides = document.querySelectorAll(".slide");
    let current = 0;

    function nextSlide() {
        slides[current].classList.remove("active");
        current = (current + 1) % slides.length;
        slides[current].classList.add("active");
    }

    setInterval(nextSlide, 5000);
});

// Car Popup
function openCarPopup(carId) {
    document.getElementById(`carPopup_${carId}`).style.display = 'block';
}
function closeCarPopup(carId) {
    document.getElementById(`carPopup_${carId}`).style.display = 'none';
}


function toggleMenu() {
    const nav = document.getElementById("navLinks");
    nav.style.display = nav.style.display === "flex" ? "none" : "flex";
}

function toggleProfileMenu() {
    const menu = document.getElementById("profileMenu");
    menu.style.display = menu.style.display === "block" ? "none" : "block";
}

// Close menus when clicking outside
window.onclick = function (event) {
    if (!event.target.matches('.fas.fa-user-circle')) {
        let dropdowns = document.getElementsByClassName("profile-menu");
        for (let i = 0; i < dropdowns.length; i++) {
            dropdowns[i].style.display = "none";
        }
    }
}




//About js
    
document.addEventListener("DOMContentLoaded", function () {
    const counters = document.querySelectorAll(".stat-number");

    const options = {
        root: null,
        rootMargin: "0px",
        threshold: 0.5 // 50% visible
    };

    const startCount = (counter) => {
        counter.innerText = "0"; // reset before counting
        const updateCount = () => {
            const target = +counter.getAttribute("data-target");
            const count = +counter.innerText.replace(/,/g, '');
            const increment = target / 500; // slow speed

            if (count < target) {
                counter.innerText = Math.ceil(count + increment).toLocaleString();
                setTimeout(updateCount, 15);
            } else {
                counter.innerText = target.toLocaleString() + "+";
            }
        };
        updateCount();
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                startCount(entry.target);
                // do not unobserve → will animate every time visible
            }
        });
    }, options);

    counters.forEach(counter => {
        observer.observe(counter);
    });
});



//Nav bar
 
window.addEventListener("scroll", function () {
    const navbar = document.querySelector("nav.navbar");
    if (window.scrollY > 20) {
        navbar.classList.add("scrolled");
    } else {
        navbar.classList.remove("scrolled");
    }
});
