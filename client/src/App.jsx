import { useState } from 'react'
import AuthPage from './components/AuthPage.jsx'
import MoviesPage from './components/MoviesPage.jsx'

const STORAGE_KEY = 'films-user'

export default function App() {
  const [user, setUser] = useState(() => JSON.parse(localStorage.getItem(STORAGE_KEY) ?? 'null'))

  const handleLogin = (u) => {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(u))
    setUser(u)
  }

  const handleLogout = () => {
    localStorage.removeItem(STORAGE_KEY)
    setUser(null)
  }

  return user ? <MoviesPage user={user} onLogout={handleLogout} /> : <AuthPage onLogin={handleLogin} />
}
