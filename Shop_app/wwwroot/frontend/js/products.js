async function login(email, password) {
            try {
                const response = await fetch(
                    "https://localhost:7089/api/apiuser/auth",
                    {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify({
                            "email": email,
                            "password": password
                        })
                    });
                if (!response.ok) {
                    throw new Error(`Error auth: ${response.status}`)
                }
                const data = await response.json()
                return data.token;
            } catch (error) {
                console.error(`Error: ${error}`)
            }
        }
        async function getProducts(token) {
            const auth_token = `Bearer ${token.result}`;
            const url = `https://localhost:7089/api/apiproduct`;
            fetch(url, {
                method: 'GET',
                headers: {
                    "Authorization": auth_token,
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
            //Auth get token
            const token = await login("admin@gmail.com", "0000")
            getProducts(token)
        }
        main()