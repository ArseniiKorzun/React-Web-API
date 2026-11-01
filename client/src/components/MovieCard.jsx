import { useState } from 'react'

export default function MovieCard({ movie, onRemove }) {
  const [imageFailed, setImageFailed] = useState(false)
  const showImage = movie.photoPath && !imageFailed

  return (
    <div className="movie-card">
      <div className="movie-poster">
        {showImage
          ? <img src={movie.photoPath} alt={movie.title} onError={() => setImageFailed(true)} />
          : <span className="poster-placeholder">🎞️</span>}
        <span className="runtime-badge">{movie.runtime} min</span>
      </div>
      <div className="movie-body">
        <h3 className="movie-title">{movie.title}</h3>
        <p className="movie-meta">by {movie.director}</p>
        {movie.description && <p className="movie-description">{movie.description}</p>}
      </div>
      <button className="remove" onClick={onRemove}>Remove ✕</button>
    </div>
  )
}
