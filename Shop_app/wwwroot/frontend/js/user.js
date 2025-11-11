document.addEventListener('DOMContentLoaded', () => {
    var user_profile = document.getElementById("user_profile")
    user_profile.style.display = "none";
    session_user()
})

async function login() {
    const email = document.getElementById("emailId").value;
    const password = document.getElementById("passwordId").value;
    if (!email || !password) {
        throw new Error(`email or password are ampty ...`);
    }
    await fetch(
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
        }).then(response => {
            if (!response.ok) {
                throw new Error(`Error auth: ${response.status}`)
            }
            return response.json()
        }).then(data => {
            localStorage.setItem("jwt_token", data.token.result)
            session_user()
            return data.token.result;
        }).catch(err => console.log(err))
}
async function register() {
    const username = document.getElementById("username_register_id").value;
    const password = document.getElementById("password_register_id").value;
    try {
        const response = await fetch(
            "https://localhost:7089/api/apiuser/register",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    "email": username,
                    "password": password
                })
            });
        if (!response.ok) {
            throw new Error(`Error auth: ${response.status}`)
        }
        const data = await response.json()
        console.log(data)
    } catch (error) {
        console.error(`Error: ${error}`)
    }
}

function parseJwt(token) {
    try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(
            atob(base64).split('').map(c =>
                '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
            ).join('')
        );
        return JSON.parse(jsonPayload);
    } catch (e) {
        return null;
    }
}

async function user_profile() {
    const user_profile_div = document.getElementById("user_profile");

    // Отримуємо токен (наприклад, з localStorage)
    const token = localStorage.getItem("jwt_token");
    if (!token) {
        user_profile_div.innerHTML = `<p style="color:red;">JWT токен не знайдено</p>`;
        return;
    }

    const userData = parseJwt(token);
    if (!userData) {
        user_profile_div.innerHTML = `<p style="color:red;">Помилка при розборі токена</p>`;
        return;
    }

    // Формуємо HTML картки профілю
    let fields = '';
    for (const [key, value] of Object.entries(userData)) {
        fields += `
      <div style="display: flex; justify-content: space-between; border-bottom: 1px solid #ccc; padding: 4px 0;">
        <strong>${key}</strong>
        <span>${value}</span>
      </div>
    `;
    }

    user_profile_div.innerHTML = `
    <div style="
      max-width: 400px;
      margin: 20px auto;
      padding: 16px;
      border-radius: 12px;
      background: #f9f9f9;
      box-shadow: 0 4px 12px rgba(0,0,0,0.1);
      font-family: Arial, sans-serif;
    ">
      <h2 style="text-align:center; color:#333;">Профіль користувача</h2>
      ${fields}
    </div>
  `;
} 

async function session_user() {
    //Дізнатися з токену інформацію про користувача
    // Приклад використання:
    const token = localStorage.getItem('jwt_token');
    const payload = parseJwt(token);
    console.log('Це користувач:', payload);
    if (token) {
        var user_profile_div = document.getElementById("user_profile")
        user_profile_div.style.display = "block";
        await user_profile()
        var box_register = document.getElementById("box_register")
        box_register.style.display = "none";
        var box_login = document.getElementById("box_login")
        box_login.style.display = "none";
    }

}

async function logout() {
    localStorage.removeItem("jwt_token");
    window.location.reload()
}