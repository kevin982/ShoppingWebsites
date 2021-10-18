
const changeTotal = (id, price) => {
    const total = document.getElementById(`total-${id}`);
    const newAmount = document.getElementById(`amount-${id}`).value;

    total.textContent = `Total: ${(price * newAmount).toFixed(2)}`;
}

const buy = async (id, price) => {
    const amount = document.getElementById(`amount-${id}`).value;

    const body = {
        id: id,
        amount: amount,
        price: price
    };

    const config = {
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        method: "POST",
        body: JSON.stringify(body)
    }

    const response = await fetch("https://localhost:9000/v1/Cart", config);

    const result = await response.json();

    if (result.succeeded){
        Swal.fire({
            background: 'black',
            icon: 'success',
            title: 'New purchase',
            text: 'Product has been bought!',
            customClass: {
                'title': 'text-white',
                'text': 'text-white'
            }
        })

        return;
    }

    Swal.fire({
        background:'black',
        icon: 'error',
        title: 'Error',
        text: result.title,
        customClass: {
            'title':'text-white',
            'text':'text-white'
        }
    })

}


const deleteProduct = async (id) => {

    try {

        Swal.fire({
            background: 'black',
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            customClass: {
                'title': 'text-white',
                'text': 'text-white'
            }
        }).then(async (result) => {
            if (result.isConfirmed) {

                const result = await fetch(`https://localhost:9000/v1/Cart/${id}`, { method: 'DELETE' });

                const response = await result.json();

                if (response.statusCode !== 200) throw new Error(response.title);

                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'The product has been removed!',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        })


    } catch (e) {
        Swal.fire({
            background: 'black',
            icon: 'error',
            title: 'Error',
            text: e.message,
            customClass: {
                'title': 'text-white',
                'text': 'text-white'
            }
        })
    }
}