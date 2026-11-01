import { useEffect, useState } from 'react'
import { api } from '../api.js'
import MovieCard from './MovieCard.jsx'
import AddMovieForm from './AddMovieForm.jsx'

export default function MoviesPage({ user, onLogout }) {
  const [movies, setMovies] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)
  const [showForm, setShowForm] = useState(false)

  useEffect(() => {
    api.getMovies()
      .then(setMovies)
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false))
  }, [])

  const handleAdd = async (data) => {
    const movie = await api.addMovie(data)
    setMovies((prev) => [...prev, movie])
    setShowForm(false)
  }

  const handleRemove = async (id) => {
    setError(null)
    try {
      await api.deleteMovie(id)
      setMovies((prev) => prev.filter((m) => m.id !== id))
    } catch (err) {
      setError(err.message)
    }
  }

  return (
    <div className="movies-page">
      <header className="movies-header">
        <div className="header-left">
          <div className="brand">🎬 Films</div>
          <button onClick={() => setShowForm(true)}>+ Add Film</button>
        </div>
        <div className="user-info">
          <span className="avatar">{user.name?.[0]?.toUpperCase()}</span>
          <span>{user.name}</span>
          <button className="secondary" onClick={onLogout}>Logout</button>
        </div>
      </header>

      {showForm && <AddMovieForm onSubmit={handleAdd} onCancel={() => setShowForm(false)} />}
      {error && <p className="message error">{error}</p>}

      {loading ? (
        <p>Loading...</p>
      ) : movies.length === 0 ? (
        <p className="empty">No films yet. Click "Add Film" to add the first one.</p>
      ) : (
        <div className="movies-grid">
          {movies.map((m) => (
            <MovieCard key={m.id} movie={m} onRemove={() => handleRemove(m.id)} />
          ))}
        </div>
      )}
    </div>
  )
}
