
document.querySelectorAll(".info textarea").forEach((el) => {
    el.addEventListener("input", () => {
        el.style.height = "auto";
        el.style.height = el.scrollHeight + "px";
    });
});

document.getElementById("ph").addEventListener("change", function (event) {
    preview.src = URL.createObjectURL(event.target.files[0]);
});

