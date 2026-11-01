import { useState } from 'react'

const empty = { title: '', director: '', runtime: '', photoPath: '', description: '' }

export default function AddMovieForm({ onSubmit, onCancel }) {
  const [form, setForm] = useState(empty)
  const [error, setError] = useState(null)
  const [saving, setSaving] = useState(false)

  const change = (e) => setForm({ ...form, [e.target.name]: e.target.value })

  const submit = async (e) => {
    e.preventDefault()
    setSaving(true)
    setError(null)
    try {
      await onSubmit({ ...form, runtime: Number(form.runtime) })
    } catch (err) {
      setError(err.message)
    } finally {
      setSaving(false)
    }
  }

  return (
    <div className="modal-backdrop" onClick={onCancel}>
      <form className="modal" onSubmit={submit} onClick={(e) => e.stopPropagation()}>
        <h2>Add Film</h2>
        <input name="title" placeholder="Title" value={form.title} onChange={change} required maxLength={100} />
        <input name="director" placeholder="Director" value={form.director} onChange={change} required maxLength={100} />
        <input name="runtime" type="number" min={1} max={600} placeholder="Runtime (min)" value={form.runtime} onChange={change} required />
        <input name="photoPath" placeholder="Poster URL" value={form.photoPath} onChange={change} maxLength={500} />
        <textarea name="description" placeholder="Description" value={form.description} onChange={change} maxLength={1000} rows={4} />
        {error && <p className="message error">{error}</p>}
        <div className="auth-buttons">
          <button type="submit" disabled={saving}>Save</button>
          <button type="button" className="secondary" onClick={onCancel}>Cancel</button>
        </div>
      </form>
    </div>
  )
}
