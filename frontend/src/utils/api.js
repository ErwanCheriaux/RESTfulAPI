export async function getBikesAsync() {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/bikes`);
        if (response.ok) {
            const data = await response.json();
            return data;
        } else {
            console.error('Failed to fetch data from API:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
    return [];
}

export async function getBikeAsync(id) {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/bikes/${id}`);
        if (response.ok) {
            const data = await response.json();
            return data;
        } else {
            console.error('Failed to fetch data from API:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

export async function postBikeAsync(newBike) {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/bikes`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newBike),
        });

        if (response.ok) {
            const createdBike = await response.json();
            return createdBike;
        } else {
            console.error('Failed to create bike:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

export async function putBikeAsync(updatedBike) {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/bikes/${updatedBike.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedBike),
        });

        if (response.ok) {
            return true;
        } else {
            console.error('Failed to update bike:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
    return false;
}

export async function deleteBikeAsync(id) {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/bikes/${id}`, {
            method: 'DELETE',
        });

        if (response.ok) {
            return true;
        } else {
            console.error('Failed to delete bike:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
    return false;
}

export async function getRidersAsync() {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/riders`);
        if (response.ok) {
            const data = await response.json();
            return data;
        } else {
            console.error('Failed to fetch data from API:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
    return [];
}

export async function getRiderAsync(id) {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/riders/${id}`);
        if (response.ok) {
            const data = await response.json();
            return data;
        } else {
            console.error('Failed to fetch data from API:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
    return [];
}

export async function postRiderAsync(newRider) {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/riders`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newRider),
        });

        if (response.ok) {
            const createdRider = await response.json();
            return createdRider;
        } else {
            console.error('Failed to create rider:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

export async function putRiderAsync(updatedRider) {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/riders/${updatedRider.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedRider),
        });

        if (response.ok) {
            return true;
        } else {
            console.error('Failed to update rider:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
    return false;
}

export async function deleteRiderAsync(id) {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/riders/${id}`, {
            method: 'DELETE',
        });

        if (response.ok) {
            return true;
        } else {
            console.error('Failed to delete rider:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
    return false;
}

export async function getRiderBikesAsync(id) {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/riders/${id}/bikes`);
        if (response.ok) {
            const data = await response.json()
            return data;
        } else {
            console.error('Failed to fetch data from API:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
    return [];
}

export async function patchRiderBikeAsync(riderId, bikeId) {
    try {
        const response = await fetch(`${process.env.REACT_APP_SERVER_URL}/riders/${riderId}/bikes?bike_id=${bikeId}`, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if (response.ok) {
            return true;
        } else {
            console.error('Failed to update rider:', response.statusText);
        }
    } catch (error) {
        console.error('Error:', error);
    }
    return false;
}
