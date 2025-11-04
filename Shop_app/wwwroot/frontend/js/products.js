async function getProducts() {
            const url = `https://localhost:7089/api/apiproduct`;
            fetch(url, {
                method: 'GET',
                headers: {
                    "Content-Type": "application/json"
                }
            })
                .then(res => {
                    if (!res.ok) {
                        throw new Error(`Error: ${res.status}`)
                    }
                    return res.json();
                })
                .then(products => {
                    const parent_div = document.getElementById("product_list")
                    parent_div.innerHTML = "";
                    products.forEach(product => {
                        const div = document.createElement("div")
                        div.className = "product_item"
                        div.innerHTML = `
                        <img src="${product.image || './img/no_image.jpeg'}" alt="${product.name}">
                        <h3>${product.name}</h3>
                        <p>${product.description}</p>
                        <strong>${product.price}</strong>
                    `
                        parent_div.appendChild(div);
                    });
                })
        }
        async function main() {
            getProducts()
        }
        main()