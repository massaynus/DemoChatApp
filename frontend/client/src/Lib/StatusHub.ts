import * as signalR from "@microsoft/signalr";

let connection: signalR.HubConnection | null = null;

export default async function getConnection(host: string = 'http://localhost:5002'): Promise<signalR.HubConnection> {

    if (connection) return connection

    connection = new signalR.HubConnectionBuilder()
        .withUrl(
            `${host}/Hubs/Status`,
            {
                accessTokenFactory: () => window.sessionStorage.getItem('token') || '',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${window.sessionStorage.getItem('token') || ''}`
                }
            }
        )
        .withAutomaticReconnect()
        .build();

    await connection.start()
    return connection
}