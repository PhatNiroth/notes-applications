const CAMBODIA_TIMEZONE = 'Asia/Phnom_Penh'

export function formatDate(input: string | Date): string {
  return new Date(input).toLocaleDateString('en-US', {
    timeZone: CAMBODIA_TIMEZONE,
    year: 'numeric',
    month: 'short',
    day: 'numeric',
  })
}

export function formatDateTime(input: string | Date): string {
  return new Date(input).toLocaleString('en-US', {
    timeZone: CAMBODIA_TIMEZONE,
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
    hour12: false,
  })
}
